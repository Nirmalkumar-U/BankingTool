import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { formatDateForInput, isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { WalletService } from '../../core/service/wallet.service';
import { DropDownDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateDepositAmountRequestObject } from '../../dto/request/bank-account/deposit-amount-request';
import { CreateDepositWalletAmountRequestObject } from '../../dto/request/wallet/deposit-wallet-amount-request';

@Component({
  selector: 'app-deposit-wallet',
  templateUrl: './deposit-wallet.component.html',
  styleUrls: ['./deposit-wallet.component.css']
})
export class DepositWalletComponent {
  depositAmountForm: FormGroup;
  accountList: DropDownDto[] = [];
  budgetAccountList: DropDownDto[] = [];
  messageList: string[] = [];
  isSubmitted: boolean = false;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private walletService: WalletService, private notificationService: NotificationService) {
    this.depositAmountForm = this.fb.group({
      accountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]],
      transferDate: ['', [Validators.required]],
      budgetAccount: this.fb.array([])
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
    this.accountList = initialData.dropDownList.find(x => x.name == "FromWalletAccount")!.dropDown;
    this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown;
  }
  depositAmount() {
    this.messageList = [];
    if (!this.depositAmountForm.invalid) {
      let budgetAccounts: any[] = this.depositAmountForm.get('budgetAccount')?.value;
      let validBudgetAccounts = budgetAccounts
        .filter(x => !isNullOrEmpty(x.budgetAccountId) && !isNullOrEmpty(x.budgetAmount))
        .map((z: any) => ({ id: z.budgetAccountId, amount: z.budgetAmount }));
      let model = CreateDepositWalletAmountRequestObject(this.depositAmountForm.get('accountId')?.value,
        this.depositAmountForm.get('amount')?.value, this.depositAmountForm.get('description')?.value,
        this.depositAmountForm.get('transferDate')?.value, validBudgetAccounts);
      this.walletService.depositWalletAmount(model).subscribe((response: ResponseDto<boolean>) => {
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
  get budgetAccount(): FormArray {
    return this.depositAmountForm.get('budgetAccount') as FormArray;
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
