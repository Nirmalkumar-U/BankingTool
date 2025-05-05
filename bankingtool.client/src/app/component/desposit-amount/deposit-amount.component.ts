import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { CreateDepositAmountRequestObject } from '../../dto/request/bank-account/deposit-amount-request';
import { ResponseDto } from '../../dto/response-dto';

@Component({
  selector: 'app-deposit-amount',
  templateUrl: './deposit-amount.component.html',
  styleUrls: ['./deposit-amount.component.css']
})
export class DepositAmountComponent {
  depositAmountForm: FormGroup;
  accountList: DropDownDto[] = [];
  messageList: string[] = [];
  isSubmitted: boolean = false;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService) {
    this.depositAmountForm = this.fb.group({
      accountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;
  }
  depositAmount() {
    this.messageList = [];
    if (!this.depositAmountForm.invalid) {
      let model = CreateDepositAmountRequestObject(this.depositAmountForm.get('accountId')?.value,
        this.depositAmountForm.get('amount')?.value, this.depositAmountForm.get('description')?.value);
      this.bankAccountService.depositAmount(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.messageList.push("Amount deposited successfully.");
        } else {
          if (Array.isArray(response?.errors) && response?.errors)
            this.messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          this.messageList.push("Transfer failed");
        }
      });
    }
  }
}
