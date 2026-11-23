import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { DashBoardAccountList, GetDashBoardInitialLoadResponse } from '../../dto/response/get-dash-board-initial-load-response';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  accountList: DashBoardAccountList[] = [];
  totalBalance: number = 0;
  totalBudgetBalance: number = 0;
  totalCreditCardUsed: number = 0;
  totalAccounts: number = 0;
  totalSavingsAccounts: number = 0;
  showBalances: boolean = false;
  searchText: string = '';
  activeFilter: string = 'All';
  currentPage: number = 1;
  readonly pageSize: number = 10;
  readonly accountFilters: string[] = ['All', 'Saving Account', 'Credit Card', 'Wallet', 'Budget Account'];

  get filteredAccounts(): DashBoardAccountList[] {
    const search = this.searchText.trim().toLowerCase();

    return this.accountList.filter(account => {
      const type = account.accountType?.toLowerCase() || '';
      const matchesFilter = this.activeFilter === 'All'
        || (this.activeFilter === 'Wallet' ? type.includes('wallet') : type === this.activeFilter.toLowerCase());
      const matchesSearch = !search
        || account.bankName?.toLowerCase().includes(search)
        || account.accountNumber?.toLowerCase().includes(search)
        || account.accountType?.toLowerCase().includes(search);

      return matchesFilter && matchesSearch;
    });
  }

  get pagedAccounts(): DashBoardAccountList[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return this.filteredAccounts.slice(startIndex, startIndex + this.pageSize);
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.filteredAccounts.length / this.pageSize));
  }

  getPageNumbers(): number[] {
    return Array.from({ length: this.totalPages }, (_, index) => index + 1);
  }

  setFilter(filter: string): void {
    this.activeFilter = filter;
    this.currentPage = 1;
  }

  onSearchChange(): void {
    this.currentPage = 1;
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  getFilterCount(filter: string): number {
    if (filter === 'All') return this.accountList.length;

    return this.accountList.filter(account => {
      const type = account.accountType?.toLowerCase() || '';
      return filter === 'Wallet' ? type.includes('wallet') : type === filter.toLowerCase();
    }).length;
  }

  isWallet(accountType: string): boolean {
    return accountType?.toLowerCase().includes('wallet') ?? false;
  }

  getAccountIcon(accountType: string): string {
    if (this.isCreditCard(accountType)) return 'bi-credit-card-fill';
    if (this.isWallet(accountType)) return 'bi-wallet2';
    if (accountType?.toLowerCase() === 'budget account') return 'bi-pie-chart-fill';
    return 'bi-bank2';
  }

  getAmountLabel(accountType: string): string {
    if (this.isCreditCard(accountType)) return 'Amount Used';
    if (this.isWallet(accountType)) return 'Wallet Balance';
    if (accountType?.toLowerCase() === 'budget account') return 'Budget Balance';
    return 'Available Balance';
  }

  getAccountAction(accountType: string): string {
    if (this.isCreditCard(accountType)) return 'Transactions';
    if (accountType?.toLowerCase() === 'budget account') return 'View Budget';
    return 'Transactions';
  }

  accountTypeMatches(accountType: string, filter: string): boolean {
    return filter === 'All' || (filter === 'Wallet' ? this.isWallet(accountType) : accountType?.toLowerCase() === filter.toLowerCase());
  }

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private readonly router: Router, private notificationService: NotificationService) {
  }
  ngOnInit() {
    const initialData: ResponseDto<GetDashBoardInitialLoadResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Dashboard load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.accountList = initialData.response.dashBoardAccountList;
    this.totalBalance = initialData.response.totalBalance;
    this.totalBudgetBalance = initialData.response.totalBudgetBalance;
    this.totalCreditCardUsed = initialData.response.totalCreditCardUsed;
    this.totalAccounts = this.accountList.length;
    this.totalSavingsAccounts = this.accountList.filter(account => account.accountType.toLowerCase() === 'saving account').length;
  }
  redirectTransferAmount() {
    this.router.navigate([AppRoute.transfer]);
  }
  redirectTransactionList(accountType: string, accountId: number) {
    if (accountType == 'Saving Account')
      this.router.navigate([AppRoute.transactions]);

    if (accountType == 'Budget Account')
      this.router.navigate([AppRoute.budgetTransactionList(accountId)]);

    if (accountType == 'Credit Card')
      this.router.navigate(['/creditCardTransactions', accountId]);
  }

  isCreditCard(accountType: string): boolean {
    return accountType?.toLowerCase() === 'credit card';
  }

  getCardNumber(cardNumber: string): string {
    if (!cardNumber) return '****';
    const normalized = cardNumber.replace(/\D/g, '');
    if (normalized.length < 4) return cardNumber;
    return `**** **** **** ${normalized.slice(-4)}`;
  }
  toggleBalances() {
    this.showBalances = !this.showBalances; 
  }
}
