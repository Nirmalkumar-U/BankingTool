import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreditCardService } from '../../core/service/credit-card.service';
import { LoaderService } from '../../core/service/loader.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';

@Component({
  selector: 'app-credit-card-payment',
  templateUrl: './credit-card-payment.component.html',
  styleUrls: ['./credit-card-payment.component.css']
})
export class CreditCardPaymentComponent implements OnInit {
  creditCardId: number = 0;
  paymentForm: FormGroup;
  isLoading: boolean = false;
  isSubmitted: boolean = false;
  cardDetails: any;
  currentBalance: number = 0;
  minimumPayment: number = 0;

  paymentMethods: string[] = ['Savings Account', 'Checking Account', 'Credit Card', 'Wire Transfer'];
  showConfirmation: boolean = false;
  confirmationData: any = null;

  constructor(
    private creditCardService: CreditCardService,
    private activatedRoute: ActivatedRoute,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.paymentForm = this.fb.group({
      paymentAmount: ['', [Validators.required, Validators.min(0.01)]],
      paymentMethod: ['Savings Account', [Validators.required]],
      description: ['Payment towards credit card balance'],
      confirmationChecked: [false, [Validators.requiredTrue]]
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.creditCardId = params['cardId'] || 0;
      if (this.creditCardId > 0) {
        this.loadCardDetails();
      } else {
        this.notificationService.show('Invalid card ID', 'error');
        this.router.navigate(['/credit-card-dashboard']);
      }
    });
  }

  loadCardDetails(): void {
    this.loaderService.show();
    this.creditCardService.getCardDetails(this.creditCardId).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && response.response) {
          this.cardDetails = response.response;
          this.currentBalance = response.response.currentBalance || 0;
          this.minimumPayment = Math.ceil((this.currentBalance * 0.02) * 100) / 100; // 2% of balance
        } else {
          this.notificationService.show('Failed to load card details', 'error');
          this.router.navigate(['/credit-card-dashboard']);
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load card details', 'error');
        console.error('Error:', error);
      }
    );
  }

  quickSetPayment(type: string): void {
    let amount = 0;
    switch (type) {
      case 'minimum':
        amount = this.minimumPayment;
        break;
      case 'half':
        amount = Math.ceil((this.currentBalance / 2) * 100) / 100;
        break;
      case 'full':
        amount = this.currentBalance;
        break;
    }
    this.paymentForm.patchValue({ paymentAmount: amount });
  }

  showPaymentConfirmation(): void {
    if (!this.paymentForm.valid) {
      this.notificationService.show('Please fill in all required fields', 'error');
      return;
    }

    const paymentAmount = this.paymentForm.get('paymentAmount')?.value;
    if (paymentAmount > this.currentBalance) {
      this.notificationService.show(`Payment amount cannot exceed current balance of ${this.formatCurrency(this.currentBalance)}`, 'error');
      return;
    }

    this.confirmationData = {
      cardNumber: this.cardDetails?.maskedCardNumber,
      cardHolder: this.cardDetails?.cardHolderName,
      paymentAmount: paymentAmount,
      paymentMethod: this.paymentForm.get('paymentMethod')?.value,
      description: this.paymentForm.get('description')?.value,
      currentBalance: this.currentBalance,
      balanceAfterPayment: this.currentBalance - paymentAmount
    };
    this.showConfirmation = true;
  }

  confirmAndProcessPayment(): void {
    this.loaderService.show();
    const paymentAmount = this.paymentForm.get('paymentAmount')?.value;
    const description = this.paymentForm.get('description')?.value;

    this.creditCardService.processPayment(this.creditCardId, paymentAmount, description).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.isSubmitted = true;
          this.notificationService.show('Payment processed successfully!', 'success');
          this.showConfirmation = false;

          setTimeout(() => {
            this.router.navigate(['/credit-card-dashboard']);
          }, 2000);
        } else {
          const errors: string[] = [];
          if (Array.isArray(response?.errors)) {
            errors.push(...response?.errors?.map((x: any) => x.errorMessage));
          }
          if (Array.isArray(response?.validationErrors)) {
            errors.push(...response?.validationErrors?.map((x: any) => x.errorMessage));
          }
          if (errors.length === 0) {
            errors.push('Payment processing failed. Please try again.');
          }
          errors.forEach(err => this.notificationService.show(err, 'error'));
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('An error occurred while processing your payment', 'error');
        console.error('Error:', error);
      }
    );
  }

  cancelPayment(): void {
    this.showConfirmation = false;
  }

  resetForm(): void {
    this.paymentForm.reset({
      paymentMethod: 'Savings Account',
      description: 'Payment towards credit card balance',
      confirmationChecked: false
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value);
  }

  getPaymentPercentage(): number {
    const amount = this.paymentForm.get('paymentAmount')?.value || 0;
    return this.currentBalance > 0 ? (amount / this.currentBalance) * 100 : 0;
  }
}
