import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { formatDateForInput, isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { WalletService } from '../../core/service/wallet.service';
import { DropDownDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetAccountBalanceRequestObject } from '../../dto/request/bank-account/get-account-balance-request';
import { GetWalletBalanceRequestObject } from '../../dto/request/wallet/get-wallet-balance-request';
import { CreateWalletTransferRequestObject } from '../../dto/request/wallet/wallet-transfer-request';

@Component({
  selector: 'app-wallet-transfer',
  templateUrl: './wallet-transfer.component.html',
  styleUrls: ['./wallet-transfer.component.css']
})
export class WalletTransferComponent {
  walletTransferForm: FormGroup;

  //DropDown
  accountList: DropDownDto[] = [];
  bankAccountList: DropDownDto[] = [];
  budgetAccountList: DropDownDto[] = [];
  transactionTagList: DropDownDto[] = [];

  //internal
  messageList: string[] = [];
  isSubmitted: boolean = false;
  isToAccount: boolean = true;
  balance: number = 0;

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder,
    private walletService: WalletService, private notificationService: NotificationService) {
    this.walletTransferForm = this.fb.group({
      bankAccountId: [''],
      accountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]],
      transferDate: ['', [Validators.required]],
      transactionTag: ['', [Validators.required]],
      budgetAccount: this.fb.array([])
    });
    this.walletTransferForm.patchValue({
      transferDate: formatDateForInput(new Date()),
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Wallet Transfer page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.accountList = initialData.dropDownList.find(x => x.name == "FromWalletAccount")!.dropDown;
    this.bankAccountList = initialData.dropDownList.find(x => x.name == "Account")!.dropDown;
    this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown;
    this.transactionTagList = initialData.dropDownList.find(x => x.name == "TransactionTag")!.dropDown;
    this.updateBankAccountIdValidator();

    this.walletTransferForm.get('accountId')?.valueChanges.subscribe(accountId => {
      if (!isNullOrEmpty(accountId)) {
        let model: GetWalletBalanceRequestObject = {
          request: {
            account: {
              id: accountId
            }
          }
        }
        this.walletService.getWalletBalance(model).subscribe((response: ResponseDto<number>) => {
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
      } else {
        this.balance = 0;
      }
    });

  }
  private updateBankAccountIdValidator() {
    const bankAccountIdControl = this.walletTransferForm.get('bankAccountId');
    if (this.isToAccount) {
      bankAccountIdControl?.setValidators([Validators.required]);
    } else {
      bankAccountIdControl?.clearValidators();
    }
    bankAccountIdControl?.updateValueAndValidity();
  }
  walletTransfer() {
    this.messageList = [];
    if (!this.walletTransferForm.invalid) {
      let budgetAccounts: any[] = this.walletTransferForm.get('budgetAccount')?.value;
      let validBudgetAccounts = budgetAccounts
        .filter(x => !isNullOrEmpty(x.budgetAccountId) && !isNullOrEmpty(x.budgetAmount))
        .map((z: any) => ({ id: z.budgetAccountId, amount: z.budgetAmount }));
      let model = CreateWalletTransferRequestObject(this.walletTransferForm.get('accountId')?.value,
        this.walletTransferForm.get('amount')?.value, this.walletTransferForm.get('description')?.value,
        this.walletTransferForm.get('transferDate')?.value, validBudgetAccounts, this.isToAccount == true ? this.walletTransferForm.get('bankAccountId')?.value : null,
        this.walletTransferForm.get('transactionTag')?.value);
      this.walletService.walletTransfer(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show("Amount transfered successfully.", 'success');
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
  get budgetAccount(): FormArray {
    return this.walletTransferForm.get('budgetAccount') as FormArray;
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
  isToAccountChange() {
    this.updateBankAccountIdValidator();
  }
  setTransferDate($event: any) {
    $event.preventDefault();
    if (!isNullOrEmpty($event.target.value)) {
      this.walletTransferForm.patchValue({
        transferDate: formatDateForInput(new Date($event.target.value)),
      });
    }
    else {
      this.walletTransferForm.patchValue({
        transferDate: null
      });
    }
  }
}
