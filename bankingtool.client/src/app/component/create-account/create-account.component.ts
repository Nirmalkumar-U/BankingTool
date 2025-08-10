import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Constant } from '../../constant/constant';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BankAccountService } from '../../core/service/bank-account.service';
import { CreateAccountInitialLoadDto } from '../../dto/internal-dto/create-account-initial-load-dto';
import { DropDownDto, YesOrNoDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateAccountRequestObject } from '../../dto/request/bank-account/create-account-request';
import { GetBankDetailWithoutCustomerAndAccountTypeRequestObject } from '../../dto/request/bank-account/get-bank-detail-without-customer-and-account-type-request';
import { IsCustomerHasCreditCardInThatBankRequestObject } from '../../dto/request/bank-account/is-customer-has-credit-card-in-that-bank-request';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent {
  accountForm: FormGroup;
  customerList: DropDownDto[] = [];
  accountTypeList: DropDownDto[] = [];
  bankList: DropDownDto[] = [];
  yesOrNo: YesOrNoDto[] = Constant.yesOrNo;
  isSubmitted: boolean = false;
  isCustAsCC: boolean = false;
  defaultValue: string = '';

  get customerId(): number {
    return this.accountForm.controls["customerId"].value;
  }
  get accountTypeId(): number {
    return this.accountForm.controls["accountTypeId"].value;
  }
  get bankId(): number {
    return this.accountForm.controls["bankId"].value;
  }

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService, private notificationService: NotificationService) {
    this.accountForm = this.fb.group({
      bankId: [this.defaultValue, [Validators.required]],
      accountTypeId: [this.defaultValue, [Validators.required]],
      customerId: [this.defaultValue, [Validators.required]],
      customerWantCreditCard: [this.defaultValue, [Validators.required]],
      doYouWantToChangeThisAccountToPrimaryAccount: [this.defaultValue, [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<CreateAccountInitialLoadDto> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Create Account page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error');
      });
    }
    this.customerList = initialData.dropDownList.find(x => x.name == "Customer")!.dropDown;
    this.accountTypeList = initialData.dropDownList.find(x => x.name == "AccountType")!.dropDown;

    this.accountForm.get('customerId')?.valueChanges.subscribe(customerId => {
      this.getBankDetailsDropDownWithoutCustomerAndAccountType(customerId, this.accountTypeId);
      this.accountForm.setValue({
        accountTypeId: this.defaultValue,
        bankId: this.defaultValue,
        customerWantCreditCard: false
      });
    });

    this.accountForm.get('accountTypeId')?.valueChanges.subscribe(accountTypeId => {
      this.getBankDetailsDropDownWithoutCustomerAndAccountType(this.customerId, accountTypeId);
      this.accountForm.setValue({
        bankId: this.defaultValue,
        customerWantCreditCard: false
      });
    });

    this.accountForm.get('bankId')?.valueChanges.subscribe(bankId => {
      if (!isNullOrEmpty(this.accountTypeId) && !isNullOrEmpty(this.customerId) && !isNullOrEmpty(bankId)) {
        let model: IsCustomerHasCreditCardInThatBankRequestObject = {
          request: {
            customer: {
              id: this.customerId
            },
            bank: {
              id: bankId
            }
          }
        }
        this.bankAccountService.isCustomerHasCreditCardInThatBank(model).subscribe((response: ResponseDto<boolean>) => {
          if (response.status == true) {
            this.accountForm.setValue({
              customerWantCreditCard: response.response
            });
            this.isCustAsCC = response.response;
          } else {
            let messageList: string[] = [];
            if (Array.isArray(response?.errors) && response?.errors)
              messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
            messageList.push("Check Is Customer Has CreditCard failed.");
            messageList.forEach((msg) => {
              this.notificationService.show(msg, 'error');
            });
          }
        });
      } else {
        this.isCustAsCC = false;
      }
    });
  }

  getBankDetailsDropDownWithoutCustomerAndAccountType(customerId: number, accountTypeId: number) {
    if (!isNullOrEmpty(accountTypeId) && !isNullOrEmpty(customerId)) {
      let model: GetBankDetailWithoutCustomerAndAccountTypeRequestObject = {
        request: {
          account: {
            accountTypeId: accountTypeId,
          },
          customer: {
            id: customerId
          }
        }
      }
      this.bankAccountService.getBankDetailsDropDownWithoutCustomerAndAccountType(model).subscribe((response: ResponseDto<DropDownDto[]>) => {
        if (response.status == true) {
          this.bankList = response.dropDownList.find(x => x.name == "Bank")!.dropDown;
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Get Bank Details Drop Down failed.");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      });
    } else {
      this.bankList = [];
    }
  }

  createAccount() {
    if (!this.accountForm.invalid) {
      let isValid: boolean = true;
      let values = this.accountForm.value;
      if (isNullOrEmpty(values.accountTypeId)) isValid = false;
      if (isNullOrEmpty(values.bankId)) isValid = false;
      if (isNullOrEmpty(values.customerId)) isValid = false;
      if (isNullOrEmpty(this.accountForm.get('customerWantCreditCard')?.value)) isValid = false;
      if (isNullOrEmpty(this.accountForm.get('doYouWantToChangeThisAccountToPrimaryAccount')?.value)) isValid = false;
      //Check
      let model: CreateAccountRequestObject = {
        request: {
          account: {
            accountTypeId: values.accountTypeId,
            doYouWantToChangeThisAccountToPrimaryAccount: values.doYouWantToChangeThisAccountToPrimaryAccount
          },
          bank: {
            id: values.bankId
          },
          customer: {
            customerWantCreditCard: values.customerWantCreditCard,
            id: values.customerId
          }
        }
      };
      this.bankAccountService.createAccount(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show("Account created successfully.", 'error');
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Create Account failed.");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      });
    }
  }
}
