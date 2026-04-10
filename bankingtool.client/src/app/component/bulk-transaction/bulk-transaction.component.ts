import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BulkTransactionService } from '../../core/service/bulk-transaction.service';
import { DropDownDto } from '../../dto/internal-dto/drop-down-dto';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { BulkTransactionRequest, BulkTransactionRequestObject } from '../../dto/request/bulkTransaction/bulk-transaction-request';

@Component({
  selector: 'app-bulk-transaction',
  templateUrl: './bulk-transaction.component.html',
  styleUrls: ['./bulk-transaction.component.scss']
})

export class BulkTransactionComponent implements OnInit {
  bulkTransactionForm: FormGroup;
  accountsList: DropDownDto[] = [];
  toList: DropDownDto[] = [];
  fromList: DropDownDto[] = [];
  budgetAccountsList: DropDownDto[] = [];
  transactionTagList: DropDownDto[] = [];
  transactionCatagoryList: DropDownDto[] = [];
  isSubmitted: boolean = false;
  isLoading: boolean = false;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private bulkTransactionService: BulkTransactionService,
    private notificationService: NotificationService
  ) {
    this.bulkTransactionForm = this.fb.group({
      transactions: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadInitialData();
  }

  loadInitialData(): void {
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
    if (initialData.status == true) {
      this.toList = initialData.dropDownList.find(x => x.name == "ToAccount")!.dropDown;
      this.fromList = initialData.dropDownList.find(x => x.name == "FromAccount")!.dropDown;
      this.budgetAccountsList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown;
      this.transactionCatagoryList = initialData.dropDownList.find(x => x.name == "TransactionCategory")!.dropDown;
      this.transactionTagList = initialData.dropDownList.find(x => x.name == "TransactionTag")!.dropDown;
    }
    this.addTransaction();
  }

  showErrorMessages(response: ResponseDto<any>): void {
    const messageList: string[] = [];
    if (Array.isArray(response?.errors) && response?.errors) {
      messageList.push(...response.errors.map(x => x.errorMessage));
    }
    if (Array.isArray(response?.validationErrors) && response?.validationErrors) {
      messageList.push(...response.validationErrors.map(x => x.errorMessage));
    }
    messageList.forEach((msg) => {
      this.notificationService.show(msg, 'error');
    });
  }

  get transactions(): FormArray {
    return this.bulkTransactionForm.get('transactions') as FormArray;
  }

  addTransaction(): void {
    const budgetEntriesArray = this.fb.array([]);
    this.addBudgetEntryToArray(budgetEntriesArray);

    const transactionGroup = this.fb.group({
      transactionCategory: ['', Validators.required],
      fromAccountId: [{ value: '', disabled: true }],
      toAccountId: [{ value: '', disabled: true }],
      amount: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', Validators.required],
      transactionDate: [new Date().toISOString().split('T')[0], Validators.required],
      transactionTag: ['', Validators.required],
      budgetEntries: budgetEntriesArray
    });

    // Listen to transaction category changes to enable/disable account fields
    transactionGroup.get('transactionCategory')?.valueChanges.subscribe((category) => {
      this.updateAccountFieldsVisibility(transactionGroup, category ?? "");
    });

    this.transactions.push(transactionGroup);
  }

  removeTransaction(index: number): void {
    if (this.transactions.length > 1) {
      this.transactions.removeAt(index);
    } else {
      this.notificationService.show('At least one transaction is required', 'warning');
    }
  }

  updateAccountFieldsVisibility(transactionGroup: FormGroup, category: string): void {
    const fromAccountControl = transactionGroup.get('fromAccountId');
    const toAccountControl = transactionGroup.get('toAccountId');
    this.resetControl(transactionGroup);
    if (category === 'Transfer') {
      fromAccountControl?.enable();
      toAccountControl?.enable();
      fromAccountControl?.setValidators([Validators.required]);
      toAccountControl?.setValidators([Validators.required]);
    } else if (category === 'Withdraw' || category === 'Deposit') {
      fromAccountControl?.enable();
      toAccountControl?.disable();
      fromAccountControl?.setValidators([Validators.required]);
      toAccountControl?.clearValidators();
    }

    fromAccountControl?.updateValueAndValidity({ emitEvent: false });
    toAccountControl?.updateValueAndValidity({ emitEvent: false });
  }

  resetControl(transactionGroup: FormGroup) {
    const fromAccountControl = transactionGroup.get('fromAccountId');
    const toAccountControl = transactionGroup.get('toAccountId');

    fromAccountControl?.reset();
    toAccountControl?.reset();

    //set "" value for fromAccountId and toAccountId
    fromAccountControl?.setValue("");
    toAccountControl?.setValue("");
  }

  getBudgetEntries(transactionIndex: number): FormArray {
    return this.transactions.at(transactionIndex).get('budgetEntries') as FormArray;
  }

  addBudgetEntry(transactionIndex: number): void {
    const budgetEntries = this.getBudgetEntries(transactionIndex);
    this.addBudgetEntryToArray(budgetEntries);
  }

  private addBudgetEntryToArray(budgetEntries: FormArray): void {
    budgetEntries.push(
      this.fb.group({
        budgetAccountId: ['', Validators.required],
        budgetAmount: ['', [Validators.required, Validators.min(0.01)]]
      })
    );
  }

  removeBudgetEntry(transactionIndex: number, budgetIndex: number): void {
    const budgetEntries = this.getBudgetEntries(transactionIndex);
    if (budgetEntries.length > 1) {
      budgetEntries.removeAt(budgetIndex);
    } else {
      this.notificationService.show('At least one budget entry is required per transaction', 'warning');
    }
  }

  submitTransactions(): void {
    if (!this.validateForm()) {
      return;
    }

    this.isLoading = true;
    const payload = this.buildPayload();

    this.bulkTransactionService.submitTransactions(payload).subscribe(
      (response: ResponseDto<boolean>) => {
        this.isLoading = false;
        if (response.status) {
          this.isSubmitted = true;
          this.notificationService.show('Transactions submitted successfully', 'success');
          this.resetForm();
        } else {
          this.showErrorMessages(response);
        }
      },
      (error) => {
        this.isLoading = false;
        this.notificationService.show('An error occurred while submitting transactions', 'error');
      }
    );
  }

  validateForm(): boolean {
    if (this.bulkTransactionForm.invalid) {
      this.notificationService.show('Please fill all required fields correctly', 'error');
      return false;
    }

    // Additional validation: Ensure from and to accounts are different for Transfer
    const transactionsArray = this.transactions.value;
    for (const transaction of transactionsArray) {
      if (transaction.transactionCategory === 'Transfer' &&
        transaction.fromAccountId === transaction.toAccountId) {
        this.notificationService.show('From and To accounts must be different for Transfer transactions', 'error');
        return false;
      }
    }

    return true;
  }

  buildPayload(): BulkTransactionRequestObject {
    const payload: BulkTransactionRequestObject = {
      request: []
    };

    const transactionsArray = this.transactions.value;
    transactionsArray.forEach((transaction: any) => {
      const budgetEntries = transaction.budgetEntries
        .filter((entry: any) => !isNullOrEmpty(entry.budgetAccountId) && !isNullOrEmpty(entry.budgetAmount))
        .map((entry: any) => ({
          Id: entry.budgetAccountId,
          Amount: entry.budgetAmount
        }));

      const request: BulkTransactionRequest = {
        transactionCategory: transaction.transactionCategory,
        account: {
          fromAccountId: transaction.fromAccountId || 0,
          toAccountId: transaction.toAccountId || 0
        },
        transaction: {
          amount: transaction.amount,
          description: transaction.description,
          transactionDate: transaction.transactionDate,
          transactionTag: transaction.transactionTag
        },
        budgetAccount: budgetEntries
      };

      payload.request.push(request);
    });

    return payload;
  }

  resetForm(): void {
    this.bulkTransactionForm.reset();
    this.transactions.clear();
    this.addTransaction();
    this.isSubmitted = false;
  }

  isFormValid(): boolean {
    return this.bulkTransactionForm.valid && this.transactions.length > 0;
  }
}

