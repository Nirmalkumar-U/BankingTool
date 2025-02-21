import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Constant } from '../../constant/constant';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BankAccountService } from '../../core/service/bank-account.service';
import { CreateAccountDto } from '../../dto/create-account-dto';
import { CreateAccountInitialLoadDto } from '../../dto/create-account-initial-load-dto';
import { DropDownDto, YesOrNoDto } from '../../dto/drop-down-dto';
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
    this.customerList = initialData.result.customerList;
    this.accountTypeList = initialData.result.accountTypeList;

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
        this.bankAccountService.isCustomerHasCreditCardInThatBank(this.customerId, bankId).subscribe((response: ResponseDto<boolean>) => {
          this.accountForm.setValue({
            customerWantCreditCard: response.result
          });
          this.isCustAsCC = response.result;
        });
      } else {
        this.isCustAsCC = false;
      }
    });
  }

  getBankDetailsDropDownWithoutCustomerAndAccountType(customerId: number, accountTypeId: number) {
    if (!isNullOrEmpty(accountTypeId) && !isNullOrEmpty(customerId)) {
      this.bankAccountService.getBankDetailsDropDownWithoutCustomerAndAccountType(customerId, accountTypeId).subscribe((bankList: ResponseDto<DropDownDto[]>) => {
        this.bankList = bankList.result;
      });
    } else {
      this.bankList = [];
    }
  }

  createAccount() {
    if (!this.accountForm.invalid) {
      this.messageList = [];
      let isValid: boolean = true;
      let values: CreateAccountDto = this.accountForm.value;
      if (isNullOrEmpty(values.accountTypeId)) isValid = false;
      if (isNullOrEmpty(values.bankId)) isValid = false;
      if (isNullOrEmpty(values.customerId)) isValid = false;
      if (isNullOrEmpty(this.accountForm.get('customerWantCreditCard')?.value)) isValid = false;
      if (isNullOrEmpty(this.accountForm.get('doYouWantToChangeThisAccountToPrimaryAccount')?.value)) isValid = false;

      this.bankAccountService.createAccount(values).subscribe((responce: ResponseDto<boolean>) => {
        this.messageList.push(...responce.message);
        this.isSubmitted = true;
      });
    }
  }
}
