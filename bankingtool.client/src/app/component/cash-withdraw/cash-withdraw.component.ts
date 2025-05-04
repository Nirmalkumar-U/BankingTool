import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { CreateCashWithdrawRequestObject } from '../../dto/request/bank-account/cash-withdraw-request';
import { GetAccountBalanceRequestObject } from '../../dto/request/bank-account/get-account-balance-request';
import { ResponseDto } from '../../dto/response-dto';

@Component({
  selector: 'app-cash-withdraw',
  templateUrl: './cash-withdraw.component.html',
  styleUrls: ['./cash-withdraw.component.css']
})
export class CashWithdrawComponent {
  cashWithdrawForm: FormGroup;
  accountList: DropDownDto[] = [];
  messageList: string[] = [];
  isSubmitted: boolean = false;
  balance: number = 0;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService) {
    this.cashWithdrawForm = this.fb.group({
      accountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;

    this.cashWithdrawForm.get('accountId')?.valueChanges.subscribe(accountId => {
      if (!isNullOrEmpty(accountId)) {
        let model: GetAccountBalanceRequestObject = {
          request: {
            account: {
              id: accountId
            }
          }
        }
        this.bankAccountService.getAccountBalance(model).subscribe((response: ResponseDto<number>) => {
          this.balance = response.response;
        });
      } else {
        this.balance = 0;
      }
    });
  }
  cashWithdraw() {
    this.messageList = [];
    if (!this.cashWithdrawForm.invalid) {
      let model = CreateCashWithdrawRequestObject(this.cashWithdrawForm.get('accountId')?.value,
        this.cashWithdrawForm.get('amount')?.value, this.cashWithdrawForm.get('description')?.value);
      this.bankAccountService.cashWithdraw(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
        } else {
          if (Array.isArray(response?.errors) && response?.errors)
            this.messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          this.messageList.push("Transfer failed");
        }
      });
    }
  }
}
