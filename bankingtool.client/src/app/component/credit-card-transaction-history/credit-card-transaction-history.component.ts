import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreditCardService } from '../../core/service/credit-card.service';
import { LoaderService } from '../../core/service/loader.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';

@Component({
  selector: 'app-credit-card-transaction',
  templateUrl: './credit-card-transaction-history.component.html',
  styleUrls: ['./credit-card-transaction-history.component.css']
})
export class CreditCardTransactionHistoryComponent implements OnInit {
  creditCardId: number = 0;
  transactions: any[] = [];
  filteredTransactions: any[] = [];
  isLoading: boolean = false;
  filterForm: FormGroup;

  // Pagination
  currentPage: number = 1;
  pageSize: number = 10;
  totalRecords: number = 0;
  totalPages: number = 0;

  // Filter options
  transactionTypes: string[] = ['Purchase', 'Payment', 'Fee', 'Interest', 'Reversal'];
  transactionStatuses: string[] = ['Completed', 'Pending', 'Failed', 'Reversed'];

  selectedTransaction: any = null;
  showDetailsModal: boolean = false;

  constructor(
    private creditCardService: CreditCardService,
    private activatedRoute: ActivatedRoute,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.filterForm = this.fb.group({
      searchText: [''],
      transactionType: [''],
      status: [''],
      fromDate: [''],
      toDate: ['']
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.creditCardId = params['cardId'] || 0;
      if (this.creditCardId > 0) {
        this.loadTransactions(1);
      } else {
        this.notificationService.show('Invalid card ID', 'error');
        this.router.navigate(['/credit-card-dashboard']);
      }
    });
  }

  loadTransactions(pageNumber: number = 1): void {
    if (this.creditCardId <= 0) return;

    this.loaderService.show();
    this.currentPage = pageNumber;

    this.creditCardService.getTransactionHistory(this.creditCardId, pageNumber, this.pageSize).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.transactions = response.response;
          this.totalRecords = response.response?.length || 0;
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
          this.applyFilters();
        } else {
          this.transactions = [];
          this.notificationService.show('No transactions found', 'info');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load transactions', 'error');
        console.error('Error:', error);
      }
    );
  }

  applyFilters(): void {
    const formValue = this.filterForm.value;

    this.filteredTransactions = this.transactions.filter(transaction => {
      let matches = true;

      if (formValue.searchText) {
        const searchLower = formValue.searchText.toLowerCase();
        matches = matches && (
          transaction.description?.toLowerCase().includes(searchLower) ||
          transaction.merchantName?.toLowerCase().includes(searchLower) ||
          transaction.referenceNumber?.toLowerCase().includes(searchLower)
        );
      }

      if (formValue.transactionType) {
        matches = matches && transaction.transactionType === formValue.transactionType;
      }

      if (formValue.status) {
        matches = matches && transaction.transactionStatus === formValue.status;
      }

      if (formValue.fromDate) {
        const fromDate = new Date(formValue.fromDate);
        const transactionDate = new Date(transaction.transactionDate);
        matches = matches && transactionDate >= fromDate;
      }

      if (formValue.toDate) {
        const toDate = new Date(formValue.toDate);
        toDate.setHours(23, 59, 59, 999);
        const transactionDate = new Date(transaction.transactionDate);
        matches = matches && transactionDate <= toDate;
      }

      return matches;
    });
  }

  resetFilters(): void {
    this.filterForm.reset();
    this.currentPage = 1;
    this.applyFilters();
  }

  viewTransactionDetails(transaction: any): void {
    this.selectedTransaction = transaction;
    this.showDetailsModal = true;
  }

  closeDetailsModal(): void {
    this.showDetailsModal = false;
    this.selectedTransaction = null;
  }

  reverseTransaction(transactionId: number, event: Event): void {
    event.stopPropagation();

    if (!confirm('Are you sure you want to reverse this transaction?')) {
      return;
    }

    const reason = prompt('Please enter the reason for reversing this transaction:');
    if (reason !== null && reason.trim() !== '') {
      this.loaderService.show();
      this.creditCardService.reverseTransaction(transactionId, reason).subscribe(
        (response: ResponseDto<any>) => {
          this.loaderService.hide();
          if (response.status === true) {
            this.notificationService.show('Transaction reversed successfully', 'success');
            this.loadTransactions(this.currentPage);
          } else {
            this.notificationService.show('Failed to reverse transaction', 'error');
          }
        },
        (error) => {
          this.loaderService.hide();
          this.notificationService.show('An error occurred while reversing the transaction', 'error');
        }
      );
    }
  }

  exportTransactions(): void {
    // TODO: Implement CSV export
    this.notificationService.show('Export feature coming soon', 'info');
  }

  getTransactionTypeClass(type: string): string {
    switch (type?.toLowerCase()) {
      case 'purchase':
        return 'badge bg-info';
      case 'payment':
        return 'badge bg-success';
      case 'fee':
        return 'badge bg-warning';
      case 'interest':
        return 'badge bg-danger';
      case 'reversal':
        return 'badge bg-secondary';
      default:
        return 'badge bg-primary';
    }
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'completed':
        return 'badge bg-success';
      case 'pending':
        return 'badge bg-warning';
      case 'failed':
        return 'badge bg-danger';
      case 'reversed':
        return 'badge bg-secondary';
      default:
        return 'badge bg-info';
    }
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value);
  }

  goToPage(pageNumber: number): void {
    if (pageNumber > 0 && pageNumber <= this.totalPages) {
      this.loadTransactions(pageNumber);
    }
  }
}
