import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Constant } from '../../constant/constant';
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
  isSubmitted: boolean = true;

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService) {
    this.accountForm = this.fb.group({
      bankId: ['', [Validators.required]],
      accountTypeId: ['', [Validators.required]],
      customerId: ['', [Validators.required]],
      customerWantCreditCard: ['', [Validators.required]],
      doYouWantToChangeThisAccountToPrimaryAccount: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<CreateAccountInitialLoadDto> = this.activatedRoute.snapshot.data['DataResolver'];
    this.customerList = initialData.result.customerList;
    this.accountTypeList = initialData.result.accountTypeList;
    this.bankList = initialData.result.bankList;
  }
  createAccount() {
    if (!this.accountForm.invalid) {
      this.messageList = [];
      let values: CreateAccountDto = this.accountForm.value;
      this.bankAccountService.createAccount(values).subscribe((responce: ResponseDto<boolean>) => {
        this.messageList.push(...responce.message);
        this.isSubmitted = true;
      });
    }
  }
}
