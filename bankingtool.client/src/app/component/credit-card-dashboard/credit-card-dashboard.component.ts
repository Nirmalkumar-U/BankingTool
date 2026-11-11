import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreditCardService } from '../../core/service/credit-card.service';
import { AuthService } from '../../core/service/auth.service';
import { LoaderService } from '../../core/service/loader.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { LocalStorageService } from '../../core/local-storage.service';
import { ClaimKey } from '../../constant/constant';

@Component({
  selector: 'app-credit-card-dashboard',
  templateUrl: './credit-card-dashboard.component.html',
  styleUrls: ['./credit-card-dashboard.component.css']
})
export class CreditCardDashboardComponent implements OnInit {
  creditCards: any[] = [];
  recentTransactions: any[] = [];
  accountId: number = 0;
  isLoading: boolean = false;
  totalBalance: number = 0;
  totalAvailableCredit: number = 0;

  constructor(
    private creditCardService: CreditCardService,
    private authService: AuthService,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    private router: Router,
    private localStorageService: LocalStorageService
  ) { }

  ngOnInit(): void {
    this.loadCreditCards();
  }

  loadCreditCards(): void {
    this.loaderService.show();
    this.creditCardService.getCustomerCreditCards().subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.creditCards = response.response;
          this.calculateTotals();

          if (this.creditCards.length > 0) {
            this.loadRecentTransactions(this.creditCards[0].creditCardId);
          }
        } else {
          this.creditCards = [];
          this.notificationService.show('No active credit cards found', 'info');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load credit cards', 'error');
        console.error('Error:', error);
      }
    );
  }

  loadRecentTransactions(creditCardId: number): void {
    this.creditCardService.getRecentTransactions(creditCardId, 5).subscribe(
      (response: ResponseDto<any>) => {
        if (response.status === true && Array.isArray(response.response)) {
          this.recentTransactions = response.response;
        }
      },
      (error) => {
        console.error('Error loading transactions:', error);
      }
    );
  }

  calculateTotals(): void {
    this.totalBalance = 0;
    this.totalAvailableCredit = 0;

    this.creditCards.forEach(card => {
      this.totalBalance += card.currentBalance || 0;
      this.totalAvailableCredit += card.availableCredit || 0;
    });
  }

  navigateToApply(): void {
    this.router.navigate(['/credit-card-application']);
  }

  navigateToTransactions(creditCardId: number): void {
    this.router.navigate(['/transactions'], { queryParams: { cardId: creditCardId } });
  }

  navigateToPayment(creditCardId: number): void {
    this.router.navigate(['/credit-card-payment'], { queryParams: { cardId: creditCardId } });
  }

  navigateToStatement(creditCardId: number): void {
    this.router.navigate(['/credit-card-statement'], { queryParams: { cardId: creditCardId } });
  }

  blockCard(creditCardId: number, event: Event): void {
    event.stopPropagation();

    const reason = prompt('Please enter the reason for blocking this card:');
    if (reason !== null && reason.trim() !== '') {
      this.loaderService.show();
      this.creditCardService.blockCreditCard(creditCardId, reason).subscribe(
        (response: ResponseDto<any>) => {
          this.loaderService.hide();
          if (response.status === true) {
            this.notificationService.show('Card blocked successfully', 'success');
            this.loadCreditCards();
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

  viewCardDetails(creditCardId: number): void {
    this.router.navigate(['/credit-card-details'], { queryParams: { cardId: creditCardId } });
  }

  getCardStatusBadgeClass(status: string): string {
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

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value);
  }
}
