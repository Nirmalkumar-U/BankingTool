import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { GetAccountBalanceRequestObject } from '../../dto/request/bank-account/get-account-balance-request';
import { getTransferAmountRequestObject, TransferAmountRequestObject } from '../../dto/request/bank-account/transfer-amount-request';
import { ResponseDto } from '../../dto/response-dto';
import { TransferAmountInitialLoadDto } from '../../dto/transfer-amount-initial-load-dto';

@Component({
  selector: 'app-transfer-amount',
  templateUrl: './transfer-amount.component.html',
  styleUrls: ['./transfer-amount.component.css']
})
export class TransferAmountComponent {
  transferAmountForm: FormGroup;
  toList: DropDownDto[] = [];
  fromList: DropDownDto[] = [];
  balance: number = 0;
  messageList: string[] = [];
  isSubmitted: boolean = false;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService) {
    this.transferAmountForm = this.fb.group({
      toAccountId: ['', [Validators.required]],
      fromAccountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<TransferAmountInitialLoadDto> = this.activatedRoute.snapshot.data['DataResolver'];
    this.toList = initialData.dropDownList.find(x => x.name == "ToAccount")!.dropDown;
    this.fromList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;

    this.transferAmountForm.get('fromAccountId')?.valueChanges.subscribe(fromAccountId => {
      if (!isNullOrEmpty(fromAccountId)) {
        let model: GetAccountBalanceRequestObject = {
          request: {
            account: {
              id: fromAccountId
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
  transferAmount() {
    this.messageList = [];
    if (!this.transferAmountForm.invalid) {
      let model = getTransferAmountRequestObject(this.transferAmountForm.get('fromAccountId')?.value, this.transferAmountForm.get('toAccountId')?.value,
        this.transferAmountForm.get('amount')?.value, this.transferAmountForm.get('description')?.value);
      this.bankAccountService.transferAmount(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.response) {
          this.isSubmitted = true;
        } else {
          this.messageList.push("Transfer failed");
        }
      });
    }
  }
}
