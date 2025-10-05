import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PageTitle } from '../../constant/constant';
import { formatDateForInput, isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetAccountBalanceRequestObject } from '../../dto/request/bank-account/get-account-balance-request';
import { CreateGetToAccountListExcludedByAccountIdRequestObject, GetToAccountListExcludedByAccountIdRequestObject } from '../../dto/request/bank-account/get-to-account-list-excluded-by-account-id-request';
import { getTransferAmountRequestObject } from '../../dto/request/bank-account/transfer-amount-request';

@Component({
  selector: 'app-transfer-amount',
  templateUrl: './transfer-amount.component.html',
  styleUrls: ['./transfer-amount.component.css']
})
export class TransferAmountComponent {
  transferAmountForm: FormGroup;
  toList: DropDownDto[] = [];
  fromList: DropDownDto[] = [];
  budgetAccountList: DropDownDto[] = [];
  transactionTagList: DropDownDto[] = [];
  balance: number = 0;
  isSubmitted: boolean = false;
  reportName: string = '';
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder,
    private bankAccountService: BankAccountService, private notificationService: NotificationService) {
    let routeData: any = this.activatedRoute.snapshot.data;
    this.reportName = routeData.reportName;
    this.transferAmountForm = this.fb.group({
      toAccountId: ['', [Validators.required]],
      fromAccountId: ['', [Validators.required]],
      transferDate: ['', [Validators.required]],
      amount: ['', [Validators.required, Validators.min(0), Validators.pattern(/^\d+(\.\d{1,2})?$|^$/)]],
      description: ['', [Validators.required]],
      transactionTag: ['', [Validators.required]],
      budgetAccount: this.fb.array([])
    });
    this.transferAmountForm.patchValue({
      transferDate: formatDateForInput(new Date()),
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Transfer amount page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    if (this.reportName == PageTitle.transfer) {
      this.toList = initialData.dropDownList.find(x => x.name == "ToAccount")!.dropDown;
      this.fromList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;
      this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown;
    }
    else if (this.reportName == PageTitle.selfTransfer) {
      this.fromList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;
    }

    this.transactionTagList = initialData.dropDownList.find(x => x.name == "TransactionTag")!.dropDown;

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
          if (response.status == true) {
            this.balance = response.response;
          } else {
            let messageList: string[] = [];
            if (Array.isArray(response?.errors) && response?.errors)
              messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
            messageList.push("Get Account Balance failed");
            messageList.forEach((msg) => {
              this.notificationService.show(msg, 'error')
            });
          }
        });
        if (this.reportName == PageTitle.selfTransfer) {
          let model: GetToAccountListExcludedByAccountIdRequestObject = CreateGetToAccountListExcludedByAccountIdRequestObject(fromAccountId)
          this.bankAccountService.getToAccountListExcludedByAccountId(model).subscribe((response: ResponseDto<boolean>) => {
            if (response.status == true) {
              this.toList = response.dropDownList.find(x => x.name == "ToAccount")!.dropDown;
            } else {
              let messageList: string[] = [];
              if (Array.isArray(response?.errors) && response?.errors)
                messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
              messageList.push("Get Account Balance failed.");
              messageList.forEach((msg) => {
                this.notificationService.show(msg, 'error');
              });
            }
          });
        }
      } else {
        this.balance = 0;
      }
    });
  }
  transferAmount() {
    if (!this.transferAmountForm.invalid) {
      let budgetAccounts: any[] = this.transferAmountForm.get('budgetAccount')?.value;
      let validBudgetAccounts = budgetAccounts
        .filter(x => !isNullOrEmpty(x.budgetAccountId) && !isNullOrEmpty(x.budgetAmount))
        .map((z: any) => ({ id: z.budgetAccountId, amount: z.budgetAmount }));
      let model = getTransferAmountRequestObject(this.transferAmountForm.get('fromAccountId')?.value, this.transferAmountForm.get('toAccountId')?.value,
        this.transferAmountForm.get('amount')?.value, this.transferAmountForm.get('description')?.value, this.transferAmountForm.get('transferDate')?.value,
        this.transferAmountForm.get('transactionTag')?.value, validBudgetAccounts);
      this.bankAccountService.transferAmount(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show('Amount transfered successfully.', 'success');
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Transfer failed");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error')
          });
        }
      });
    }
  }
  setTransferDate($event: any) {
    $event.preventDefault();
    if (!isNullOrEmpty($event.target.value))
      this.transferAmountForm.patchValue({
        transferDate: formatDateForInput(new Date($event.target.value)),
      });
    else
      this.transferAmountForm.patchValue({
        transferDate: null
      });
  }
  get budgetAccount(): FormArray {
    return this.transferAmountForm.get('budgetAccount') as FormArray;
  }

  addBudgetAccount(): void {
    this.budgetAccount.push(
      this.fb.group({
        budgetAccountId: ['', Validators.required],
        budgetAmount: ['', [Validators.required, Validators.min(0), Validators.pattern(/^\d+(\.\d{1,2})?$|^$/)]]
      })
    );
  }

  removeBudgetAccount(index: number): void {
    this.budgetAccount.removeAt(index);
  }
}
