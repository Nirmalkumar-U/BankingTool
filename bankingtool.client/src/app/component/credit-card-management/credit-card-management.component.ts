import { Component, OnInit } from '@angular/core';
import { CreditCardService } from '../../core/service/credit-card.service';
import { LoaderService } from '../../core/service/loader.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-credit-card-management',
  templateUrl: './credit-card-management.component.html',
  styleUrls: ['./credit-card-management.component.css']
})
export class CreditCardManagementComponent implements OnInit {
  creditCards: any[] = [];
  filteredCreditCards: any[] = [];
  isLoading: boolean = false;
  searchText: string = '';

  selectedCard: any = null;
  showManagementModal: boolean = false;
  managementForm: FormGroup;

  filterStatus: string = '';
  statuses: string[] = ['Active', 'Blocked', 'Closed', 'Expired', 'Pending'];

  currentPage: number = 1;
  pageSize: number = 10;
  totalCards: number = 0;
  totalPages: number = 0;

  constructor(
    private creditCardService: CreditCardService,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    private fb: FormBuilder
  ) {
    this.managementForm = this.fb.group({
      cardLimit: ['', [Validators.required, Validators.min(1000), Validators.max(100000)]],
      reason: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadAllCreditCards();
  }

  loadAllCreditCards(): void {
    this.loaderService.show();
    this.creditCardService.getPendingApplications().subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.creditCards = response.response;
          this.totalCards = this.creditCards.length;
          this.totalPages = Math.ceil(this.totalCards / this.pageSize);
          this.applyFilters();
        } else {
          this.creditCards = [];
          this.notificationService.show('No credit cards found', 'info');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load credit cards', 'error');
        console.error('Error:', error);
      }
    );
  }

  applyFilters(): void {
    this.filteredCreditCards = this.creditCards.filter(card => {
      let matches = true;

      if (this.searchText) {
        const searchLower = this.searchText.toLowerCase();
        matches = matches && (
          card.cardHolderName?.toLowerCase().includes(searchLower) ||
          card.maskedCardNumber?.toLowerCase().includes(searchLower) ||
          card.cardType?.toLowerCase().includes(searchLower)
        );
      }

      if (this.filterStatus) {
        matches = matches && card.status === this.filterStatus;
      }

      return matches;
    });
  }

  updateCardLimit(card: any): void {
    if (!this.managementForm.valid) {
      this.notificationService.show('Please fill all required fields', 'error');
      return;
    }

    const newLimit = this.managementForm.get('cardLimit')?.value;
    this.loaderService.show();
    this.creditCardService.updateCardLimit(card.creditCardId, newLimit).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.notificationService.show(`Card limit updated to ${this.formatCurrency(newLimit)}`, 'success');
          this.closeManagementModal();
          this.loadAllCreditCards();
        } else {
          this.notificationService.show('Failed to update card limit', 'error');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('An error occurred while updating card limit', 'error');
        console.error('Error:', error);
      }
    );
  }

  blockCard(card: any): void {
    this.selectedCard = card;
    this.managementForm.reset();
    this.managementForm.patchValue({ reason: '' });

    const reason = prompt('Enter the reason for blocking this card:');
    if (reason !== null && reason.trim() !== '') {
      this.loaderService.show();
      this.creditCardService.blockCreditCard(card.creditCardId, reason).subscribe(
        (response: ResponseDto<any>) => {
          this.loaderService.hide();
          if (response.status === true) {
            this.notificationService.show('Card blocked successfully', 'success');
            this.loadAllCreditCards();
          } else {
            this.notificationService.show('Failed to block card', 'error');
          }
        },
        (error) => {
          this.loaderService.hide();
          this.notificationService.show('An error occurred while blocking the card', 'error');
        }
      );
    }
  }

  unblockCard(card: any): void {
    if (!confirm('Are you sure you want to unblock this card?')) {
      return;
    }

    this.loaderService.show();
    this.creditCardService.unblockCreditCard(card.creditCardId).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.notificationService.show('Card unblocked successfully', 'success');
          this.loadAllCreditCards();
        } else {
          this.notificationService.show('Failed to unblock card', 'error');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('An error occurred while unblocking the card', 'error');
      }
    );
  }

  closeCard(card: any): void {
    const reason = prompt('Enter the reason for closing this card:');
    if (reason !== null && reason.trim() !== '') {
      this.loaderService.show();
      this.creditCardService.closeCreditCard(card.creditCardId, reason).subscribe(
        (response: ResponseDto<any>) => {
          this.loaderService.hide();
          if (response.status === true) {
            this.notificationService.show('Card closed successfully', 'success');
            this.loadAllCreditCards();
          } else {
            this.notificationService.show('Failed to close card', 'error');
          }
        },
        (error) => {
          this.loaderService.hide();
          this.notificationService.show('An error occurred while closing the card', 'error');
        }
      );
    }
  }

  openLimitManagementModal(card: any): void {
    this.selectedCard = card;
    this.managementForm.reset({
      cardLimit: card.creditLimit,
      reason: ''
    });
    this.showManagementModal = true;
  }

  closeManagementModal(): void {
    this.showManagementModal = false;
    this.selectedCard = null;
    this.managementForm.reset();
  }

  searchCards(): void {
    this.currentPage = 1;
    this.applyFilters();
  }

  filterByStatus(status: string): void {
    this.filterStatus = status;
    this.currentPage = 1;
    this.applyFilters();
  }

  resetFilters(): void {
    this.searchText = '';
    this.filterStatus = '';
    this.currentPage = 1;
    this.applyFilters();
  }

  goToPage(pageNumber: number): void {
    if (pageNumber > 0 && pageNumber <= this.totalPages) {
      this.currentPage = pageNumber;
    }
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active':
        return 'badge bg-success';
      case 'blocked':
        return 'badge bg-warning';
      case 'closed':
        return 'badge bg-secondary';
      case 'expired':
        return 'badge bg-danger';
      default:
        return 'badge bg-info';
    }
  }

  canModifyCard(status: string): boolean {
    return status?.toLowerCase() === 'active' || status?.toLowerCase() === 'pending';
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value);
  }

  getActiveCardsCount(): number {
    return this.creditCards.filter(c => c.status === 'Active').length;
  }

  getBlockedCardsCount(): number {
    return this.creditCards.filter(c => c.status === 'Blocked').length;
  }

  getClosedCardsCount(): number {
    return this.creditCards.filter(c => c.status === 'Closed').length;
  }
}
