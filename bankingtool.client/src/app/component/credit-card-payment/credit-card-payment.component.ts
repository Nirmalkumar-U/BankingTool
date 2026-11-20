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
  customerCards: any[] = [];
  paymentForm: FormGroup;
  merchantPaymentForm: FormGroup;
  paymentMode: 'card' | 'merchant' = 'card';
  isLoading: boolean = false;
  isSubmitted: boolean = false;
  cardDetails: any;
  currentBalance: number = 0;
  minimumPayment: number = 0;

  paymentMethods: string[] = ['Savings Account', 'Checking Account', 'Credit Card', 'Wire Transfer'];
  merchantTypes: string[] = ['Payment', 'Purchase','Bill Payment'];
  isMerchantConfirmation: boolean = false;
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
      creditCardId: [0, [Validators.required, Validators.min(1)]],
      paymentAmount: ['', [Validators.required, Validators.min(0.01)]],
      paymentMethod: ['Savings Account', [Validators.required]],
      description: ['Payment towards credit card balance'],
      confirmationChecked: [false, [Validators.requiredTrue]]
    });

    this.merchantPaymentForm = this.fb.group({
      creditCardId: [0, [Validators.required, Validators.min(1)]],
      merchantName: ['', [Validators.required]],
      paymentAmount: ['', [Validators.required, Validators.min(0.01)]],
      paymentType: ['Purchase', [Validators.required]],
      description: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      const cardIdParam = params['cardId'];
      this.creditCardId = cardIdParam ? Number(cardIdParam) : 0;
      this.loadCustomerCards();
    });
  }

  loadCustomerCards(): void {
    this.loaderService.show();
    this.creditCardService.getCustomerCreditCards().subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.customerCards = response.response.map(card => ({
            ...card,
            maskedCardNumber: card.cardNumber?.maskedCardNumber || card.maskedCardNumber || '****'
          }));

          this.customerCards = this.customerCards.filter(card =>
            card.status?.toLowerCase() !== 'closed' &&
            card.status?.toLowerCase() !== 'blocked'
          );

          if (this.customerCards.length === 0) {
            this.cardDetails = null;
            this.currentBalance = 0;
            this.minimumPayment = 0;
            this.selectedCreditCardId = 0;
            this.paymentForm.patchValue({ creditCardId: 0 });
            this.notificationService.show('No active credit cards available for payment', 'info');
            return;
          };

          if (this.customerCards.length === 0) {
            this.cardDetails = null;
            this.currentBalance = 0;
            this.minimumPayment = 0;
            this.selectedCreditCardId = 0;
            this.paymentForm.patchValue({ creditCardId: 0 });
            this.notificationService.show('No credit cards available for payment', 'info');
            return;
          }

          const selectedId = this.creditCardId > 0
            ? this.customerCards.find(card => Number(card.creditCardId) === this.creditCardId)?.creditCardId ?? this.customerCards[0].creditCardId
            : this.customerCards[0].creditCardId;

          this.selectedCreditCardId = Number(selectedId);
          this.paymentForm.patchValue({ creditCardId: this.selectedCreditCardId });
          this.merchantPaymentForm.patchValue({ creditCardId: this.selectedCreditCardId });
          this.loadCardDetails(this.selectedCreditCardId);
        } else {
          this.customerCards = [];
          this.cardDetails = null;
          this.currentBalance = 0;
          this.minimumPayment = 0;
          this.paymentForm.patchValue({ creditCardId: 0 });
          this.notificationService.show('No credit cards available for payment', 'info');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load credit cards', 'error');
        console.error('Error loading credit cards:', error);
      }
    );
  }

  selectedCreditCardId: number = 0;

  onCardSelectionChange(): void {
    const selectedId = Number(this.paymentForm.get('creditCardId')?.value || 0);
    this.selectedCreditCardId = selectedId;
    this.creditCardId = selectedId;
    this.merchantPaymentForm.patchValue({ creditCardId: selectedId });

    if (selectedId > 0) {
      this.loadCardDetails(selectedId);
    } else {
      this.cardDetails = null;
      this.currentBalance = 0;
      this.minimumPayment = 0;
      this.showConfirmation = false;
      this.resetForm();
    }
  }

  loadCardDetails(creditCardId: number = this.creditCardId): void {
    if (!creditCardId || creditCardId <= 0) {
      return;
    }

    this.creditCardId = creditCardId;
    this.selectedCreditCardId = creditCardId;
    this.paymentForm.patchValue({ creditCardId: creditCardId });
    this.loaderService.show();
    this.creditCardService.getCardDetails(creditCardId).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && response.response) {
          this.cardDetails = {
            ...response.response,
            maskedCardNumber: response.response.cardNumber?.maskedCardNumber || response.response.maskedCardNumber || '****'
          };
          this.currentBalance = Number(response.response.limitUsed || 0);
          this.minimumPayment = Math.ceil((this.currentBalance * 0.02) * 100) / 100;
          this.paymentForm.patchValue({
            creditCardId: creditCardId,
            paymentAmount: '',
            paymentMethod: 'Savings Account',
            description: 'Payment towards credit card balance',
            confirmationChecked: false
          });
        } else {
          this.notificationService.show('Failed to load card details', 'error');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load card details', 'error');
        console.error('Error:', error);
      }
    );
  }

  resetForm(): void {
    this.paymentForm.patchValue({
      paymentAmount: '',
      paymentMethod: 'Savings Account',
      description: 'Payment towards credit card balance',
      confirmationChecked: false
    });
  }

  showPaymentConfirmation(): void {
    if (!this.paymentForm.valid) {
      this.notificationService.show('Please fill in all required fields', 'error');
      return;
    }

    const paymentAmount = Number(this.paymentForm.get('paymentAmount')?.value || 0);
    if (paymentAmount <= 0) {
      this.notificationService.show('Payment amount must be greater than zero', 'error');
      return;
    }
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
    const selectedCardId = Number(this.paymentForm.get('creditCardId')?.value || this.creditCardId);
    const paymentAmount = Number(this.paymentForm.get('paymentAmount')?.value || 0);
    const description = this.paymentForm.get('description')?.value || 'Payment towards credit card balance';

    if (selectedCardId <= 0 || paymentAmount <= 0) {
      this.loaderService.hide();
      this.notificationService.show('Select a card and enter a valid payment amount', 'error');
      return;
    }

    this.creditCardService.processPayment(selectedCardId, paymentAmount, description).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.isSubmitted = true;
          this.notificationService.show('Payment processed successfully!', 'success');
          this.showConfirmation = false;

          setTimeout(() => {
            this.router.navigate(['/creditCardDashboard']);
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
    this.isMerchantConfirmation = false;
  }

  setPaymentMode(mode: 'card' | 'merchant'): void {
    this.paymentMode = mode;
    this.showConfirmation = false;
    this.isMerchantConfirmation = false;

    if (mode === 'merchant') {
      this.merchantPaymentForm.patchValue({
        creditCardId: this.selectedCreditCardId,
        merchantName: '',
        paymentAmount: '',
        paymentType: 'Purchase',
        description: '',
        referenceNumber: ''
      });
    }
  }

  onMerchantCardSelectionChange(): void {
    const selectedId = Number(this.merchantPaymentForm.get('creditCardId')?.value || 0);
    this.selectedCreditCardId = selectedId;
    this.creditCardId = selectedId;
    this.paymentForm.patchValue({ creditCardId: selectedId });

    if (selectedId > 0) {
      this.loadCardDetails(selectedId);
    }
  }

  showMerchantPaymentConfirmation(): void {
    if (!this.merchantPaymentForm.valid) {
      this.notificationService.show('Please complete all required merchant payment fields', 'error');
      return;
    }

    const amount = Number(this.merchantPaymentForm.get('paymentAmount')?.value || 0);
    const availableCredit = Number(this.cardDetails?.availableCredit ?? 0);

    if (amount <= 0) {
      this.notificationService.show('Payment amount must be greater than zero', 'error');
      return;
    }

    if (amount > availableCredit) {
      this.notificationService.show(`Payment amount cannot exceed available credit of ${this.formatCurrency(availableCredit)}`, 'error');
      return;
    }

    this.confirmationData = {
      cardNumber: this.cardDetails?.maskedCardNumber,
      cardHolder: this.cardDetails?.cardHolderName,
      merchantName: this.merchantPaymentForm.get('merchantName')?.value,
      paymentAmount: amount,
      paymentType: this.merchantPaymentForm.get('paymentType')?.value,
      description: this.merchantPaymentForm.get('description')?.value,
      referenceNumber: this.merchantPaymentForm.get('referenceNumber')?.value || '-',
      availableCredit,
      availableAfterPayment: availableCredit - amount
    };

    this.isMerchantConfirmation = true;
    this.showConfirmation = true;
  }

  confirmMerchantPayment(): void {
    if (!this.merchantPaymentForm.valid) {
      this.notificationService.show('Please complete all required merchant payment fields', 'error');
      return;
    }

    this.loaderService.show();
    const cardId = Number(this.merchantPaymentForm.get('creditCardId')?.value || 0);
    const amount = Number(this.merchantPaymentForm.get('paymentAmount')?.value || 0);
    const description = this.merchantPaymentForm.get('description')?.value;
    const merchantName = this.merchantPaymentForm.get('merchantName')?.value;
    const referenceNumber = this.merchantPaymentForm.get('referenceNumber')?.value || '';
    const paymentType = this.merchantPaymentForm.get('paymentType')?.value || '';

    this.creditCardService.recordTransaction(cardId, paymentType, amount, description, merchantName, referenceNumber).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();

        if (response.status === true) {
          this.isSubmitted = true;
          this.showConfirmation = false;
          this.isMerchantConfirmation = false;
          this.notificationService.show('Merchant payment processed successfully!', 'success');

          setTimeout(() => this.router.navigate(['/creditCardDashboard']), 2000);
        } else {
          const errors: string[] = [];
          if (Array.isArray(response?.errors)) {
            errors.push(...response.errors.map((x: any) => x.errorMessage));
          }
          if (Array.isArray(response?.validationErrors)) {
            errors.push(...response.validationErrors.map((x: any) => x.errorMessage));
          }
          if (errors.length === 0) {
            errors.push('Merchant payment failed. Please try again.');
          }
          errors.forEach(error => this.notificationService.show(error, 'error'));
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('An error occurred while processing the merchant payment', 'error');
        console.error('Error processing merchant payment:', error);
      }
    );
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

}
