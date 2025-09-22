import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { formatDateForInput, isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { WalletService } from '../../core/service/wallet.service';
import { DropDownDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';

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
