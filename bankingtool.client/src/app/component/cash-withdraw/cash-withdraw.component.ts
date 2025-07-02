import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { formatDateForInput, isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
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
  budgetAccountList: DropDownDto[] = [];
  isSubmitted: boolean = false;
  balance: number = 0;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService, private notificationService: NotificationService) {
    this.cashWithdrawForm = this.fb.group({
      accountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]],
      transferDate: ['', [Validators.required]],
      budgetAccount: this.fb.array([])
    });
    this.cashWithdrawForm.patchValue({
      transferDate: formatDateForInput(new Date()),
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Cash withdraw page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.accountList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;
    this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown;

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
          if (response.status == true) {
            this.balance = response.response;
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
      } else {
        this.balance = 0;
      }
    });
  }
  cashWithdraw() {
    if (!this.cashWithdrawForm.invalid) {
      let budgetAccounts: any[] = this.cashWithdrawForm.get('budgetAccount')?.value;
      let validBudgetAccounts = budgetAccounts
        .filter(x => !isNullOrEmpty(x.budgetAccountId) && !isNullOrEmpty(x.budgetAmount))
        .map((z: any) => ({ id: z.budgetAccountId, amount: z.budgetAmount }));
      let model = CreateCashWithdrawRequestObject(this.cashWithdrawForm.get('accountId')?.value,
        this.cashWithdrawForm.get('amount')?.value, this.cashWithdrawForm.get('description')?.value
        , this.cashWithdrawForm.get('transferDate')?.value, validBudgetAccounts);
      this.bankAccountService.cashWithdraw(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show("Cash withdraw successfully.", 'success');
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Cash Withdraw failed.");
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
      this.cashWithdrawForm.patchValue({
        transferDate: formatDateForInput(new Date($event.target.value)),
      });
    }
    else {
      this.cashWithdrawForm.patchValue({
        transferDate: null
      });
    }
  }
  get budgetAccount(): FormArray {
    return this.cashWithdrawForm.get('budgetAccount') as FormArray;
  }

  addBudgetAccount(): void {
    this.budgetAccount.push(
      this.fb.group({
        budgetAccountId: ['', Validators.required],
        budgetAmount: ['', Validators.required]
      })
    );
  }

  removeBudgetAccount(index: number): void {
    this.budgetAccount.removeAt(index);
  }
}
