import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/drop-down-dto';
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
      amount: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<TransferAmountInitialLoadDto> = this.activatedRoute.snapshot.data['DataResolver'];
    this.toList = initialData.result.toAccount;
    this.fromList = initialData.result.fromAccount;

    this.transferAmountForm.get('fromAccountId')?.valueChanges.subscribe(fromAccountId => {
      if (!isNullOrEmpty(fromAccountId)) {
        this.bankAccountService.getAccountBalance(fromAccountId).subscribe((response: ResponseDto<number>) => {
          this.balance = response.result;
        });
      } else {
        this.balance = 0;
      }
    });
  }
  transferAmount() {

  }
}
