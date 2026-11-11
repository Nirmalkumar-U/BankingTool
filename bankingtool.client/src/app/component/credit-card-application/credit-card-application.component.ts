import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { LoaderService } from '../../core/service/loader.service';
import { CreditCardService } from '../../core/service/credit-card.service';
import { AuthService } from '../../core/service/auth.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { LocalStorageService } from '../../core/local-storage.service';
import { ClaimKey } from '../../constant/constant';
import { BankAccountService } from '../../core/service/bank-account.service';

@Component({
  selector: 'app-credit-card-application',
  templateUrl: './credit-card-application.component.html',
  styleUrls: ['./credit-card-application.component.css']
})
export class CreditCardApplicationComponent implements OnInit {
  applicationForm: FormGroup;
  isSubmitted: boolean = false;
  isLoading: boolean = false;
  cardTypes: string[] = ['Visa', 'Mastercard', 'AmericanExpress', 'Discover'];
  accounts: any[] = [];
  customerId: number = 0;

  constructor(
    private fb: FormBuilder,
    private creditCardService: CreditCardService,
    private bankAccountService: BankAccountService,
    private notificationService: NotificationService,
    private loaderService: LoaderService,
    private authService: AuthService,
    private router: Router,
    private localStorageService: LocalStorageService
  ) {
    this.applicationForm = this.fb.group({
      account: ['', [Validators.required]],
      cardType: ['Visa', [Validators.required]],
      desiredLimit: [5000, [Validators.required, Validators.min(1000), Validators.max(100000)]],
      incomeDocuments: ['', []]
    });
  }

  ngOnInit(): void {
    this.customerId = Number(this.localStorageService.getItem(ClaimKey.customerId));
    this.loadCustomerAccounts();
  }

  loadCustomerAccounts(): void {
    this.loaderService.show();
    this.bankAccountService.getCustomerAccountsForCreditCard().subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && response.dropDownList && response.dropDownList.length > 0) {
          const accountDropdown = response.dropDownList.find((x: any) => x.name === 'Account');
          if (accountDropdown && accountDropdown.dropDown) {
            this.accounts = accountDropdown.dropDown;
            // Set first account as default
            if (this.accounts.length > 0) {
              this.applicationForm.patchValue({
                account: this.accounts[0].key
              });
            }
          }
        } else {
          this.notificationService.show('No accounts found. Please create an account first.', 'error');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load accounts', 'error');
        console.error('Error loading accounts:', error);
      }
    );
  }

  applyCreditCard(): void {
    // Check if form is valid and accountId is loaded
    if (!this.applicationForm.valid) {
      this.notificationService.show('Please fill in all required fields correctly', 'error');
      return;
    }

    const selectedAccountId = this.applicationForm.get('account')?.value;
    if (!selectedAccountId || selectedAccountId <= 0) {
      this.notificationService.show('Please select a valid account.', 'error');
      return;
    }

    this.loaderService.show();

    const values = this.applicationForm.value;

    // Convert desiredLimit to number if it's a string
    const desiredLimit = typeof values.desiredLimit === 'string' 
      ? parseFloat(values.desiredLimit) 
      : values.desiredLimit;

    this.creditCardService.applyCreditCard(
      selectedAccountId,
      values.cardType,
      desiredLimit
    ).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.isSubmitted = true;
          this.notificationService.show('Credit card application submitted successfully!', 'success');
          //this.notificationService.show(`Your application ID is: ${response.data?.applicationId}`, 'info');

          // Redirect to dashboard after 2 seconds
          setTimeout(() => {
            this.router.navigate(['/credit-card-dashboard']);
          }, 2000);
        } else {
          const messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors) {
            messageList.push(...response?.errors?.map((x: any) => x.errorMessage));
          }
          if (Array.isArray(response?.validationErrors) && response?.validationErrors) {
            messageList.push(...response?.validationErrors?.map((x: any) => x.errorMessage));
          }
          if (messageList.length === 0) {
            messageList.push('Credit card application failed.');
          }
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('An error occurred while submitting your application', 'error');
        console.error('Error:', error);
      }
    );
  }

  resetForm(): void {
    const defaultAccountId = this.accounts.length > 0 ? this.accounts[0].key : '';
    this.applicationForm.reset({
      account: defaultAccountId,
      cardType: 'Visa',
      desiredLimit: 5000,
      incomeDocuments: ''
    });

  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const base64String = e.target.result;
        this.applicationForm.patchValue({
          incomeDocuments: base64String
        });
      };
      reader.readAsDataURL(file);
    }
  }
}
