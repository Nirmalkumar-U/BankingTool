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
  totalAccounts: number = 0;
  totalSavingsAccounts: number = 0;
  showBalances: boolean = false;

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
  }
  toggleBalances() {
    this.showBalances = !this.showBalances; 
  }
}
