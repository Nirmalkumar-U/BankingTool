import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { formatDateForInput, isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
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
  budgetAccountList: DropDownDto[] = [];
  messageList: string[] = [];
  isSubmitted: boolean = false;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService, private notificationService: NotificationService) {
    this.depositAmountForm = this.fb.group({
      accountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]],
      transferDate: ['', [Validators.required]],
      budgetAccountId: ['']
    });
    this.depositAmountForm.patchValue({
      transferDate: formatDateForInput(new Date()),
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Deposit amount page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.accountList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;
    this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown;
  }
  depositAmount() {
    this.messageList = [];
    if (!this.depositAmountForm.invalid) {
      let model = CreateDepositAmountRequestObject(this.depositAmountForm.get('accountId')?.value,
        this.depositAmountForm.get('amount')?.value, this.depositAmountForm.get('description')?.value,
        this.depositAmountForm.get('transferDate')?.value, this.depositAmountForm.get('budgetAccountId')?.value);
      this.bankAccountService.depositAmount(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show("Amount deposited successfully.", 'success');
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Transfer failed");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      });
    }
  }
  setTransferDate($event: any) {
    $event.preventDefault();
    if (!isNullOrEmpty($event.target.value)) {
      this.depositAmountForm.patchValue({
        transferDate: formatDateForInput(new Date($event.target.value)),
      });
    }
    else {
      this.depositAmountForm.patchValue({
        transferDate: null
      });
    }
  }
}
