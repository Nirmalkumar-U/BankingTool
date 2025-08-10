import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BudgetService } from '../../core/service/budget.service';
import { DropDownDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateGetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject } from '../../dto/request/budget/get-to-budget-dropdown-list-and-balance-of-budget-account-request';
import { CreateSaveBudgetTransactionRequestObject } from '../../dto/request/budget/save-budget-transaction-request';

@Component({
  selector: 'app-budget-transfer',
  templateUrl: './budget-transfer.component.html',
  styleUrls: ['./budget-transfer.component.css']
})
export class BudgetTransferComponent {
  transferAmountForm: FormGroup;
  fromList: DropDownDto[] = [];
  budgetAccountList: DropDownDto[] = [];
  balance: number = 0;
  isSubmitted: boolean = false;
  reportName: string = '';
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder,
    private budgetService: BudgetService, private notificationService: NotificationService) {
    let routeData: any = this.activatedRoute.snapshot.data;
    this.reportName = routeData.reportName;
    this.transferAmountForm = this.fb.group({
      fromAccountId: ['', [Validators.required]],
      amount: ['', [Validators.required]],
      description: ['', [Validators.required]],
      budgetAccount: this.fb.array([])
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
    this.fromList = initialData.dropDownList.find(x => x.name == "FromBudgetAccount")!.dropDown;
    this.transferAmountForm.get('fromAccountId')?.valueChanges.subscribe(fromAccountId => {
      if (!isNullOrEmpty(fromAccountId)) {
        let model = CreateGetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject(fromAccountId);
        this.budgetService.getToBudgetDropdownListAndBalanceOfBudgetAccount(model).subscribe((response: ResponseDto<number>) => {
          if (response.status == true) {
            this.balance = response.response;
            this.budgetAccountList = response.dropDownList.find(x => x.name == "ToBudgetAccountId")!.dropDown;
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
    this.addBudgetAccount();
  }
  transferAmount():void {
    if (!this.transferAmountForm.invalid) {
      let budgetAccounts: any[] = this.transferAmountForm.get('budgetAccount')?.value;
      let validBudgetAccounts = budgetAccounts
        .filter(x => !isNullOrEmpty(x.budgetAccountId) && !isNullOrEmpty(x.budgetAmount))
        .map((z: any) => ({ id: z.budgetAccountId, amount: z.budgetAmount }));
      let model = CreateSaveBudgetTransactionRequestObject(this.transferAmountForm.get('fromAccountId')?.value,
        this.transferAmountForm.get('amount')?.value, this.transferAmountForm.get('description')?.value, validBudgetAccounts);
      this.budgetService.saveBudgetTransaction(model).subscribe((response: ResponseDto<boolean>) => {
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
  get budgetAccount(): FormArray {
    return this.transferAmountForm.get('budgetAccount') as FormArray;
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
