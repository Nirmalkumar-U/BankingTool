import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Constant } from '../../constant/constant';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BankAccountService } from '../../core/service/bank-account.service';
import { CreateAccountInitialLoadDto } from '../../dto/create-account-initial-load-dto';
import { DropDownDto, YesOrNoDto } from '../../dto/drop-down-dto';
import { CreateAccountRequestObject } from '../../dto/request/bank-account/create-account-request';
import { GetBankDetailWithoutCustomerAndAccountTypeRequestObject } from '../../dto/request/bank-account/get-bank-detail-without-customer-and-account-type-request';
import { IsCustomerHasCreditCardInThatBankRequestObject } from '../../dto/request/bank-account/is-customer-has-credit-card-in-that-bank-request';
import { ResponseDto } from '../../dto/response-dto';

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
  messageList: string[] = [];
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

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService) {
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
          this.accountForm.setValue({
            customerWantCreditCard: response.response
          });
          this.isCustAsCC = response.response;
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
      this.bankAccountService.getBankDetailsDropDownWithoutCustomerAndAccountType(model).subscribe((bankList: ResponseDto<DropDownDto[]>) => {
        this.bankList = bankList.dropDownList.find(x => x.name == "Bank")!.dropDown;
      });
    } else {
      this.bankList = [];
    }
  }

  createAccount() {
    if (!this.accountForm.invalid) {
      this.messageList = [];
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
      this.bankAccountService.createAccount(model).subscribe((responce: ResponseDto<boolean>) => {
        this.messageList = [...responce.errors.map(x => x.errorMessage), ...responce.validationErrors.map(x => x.errorMessage)]
        this.isSubmitted = true;
      });
    }
  }
}
