import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BulkTransactionService } from '../../core/service/bulk-transaction.service';
import { ExcelImportService, ParsedTransaction } from '../../core/service/excel-import.service';
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
  isExcelImporting: boolean = false;

  // Mapping caches for Excel values to dropdown keys
  private accountValueToKeyMap: Map<string, number> = new Map();
  private tagValueToKeyMap: Map<string, number> = new Map();
  private budgetAccountValueToKeyMap: Map<string, number> = new Map();

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private bulkTransactionService: BulkTransactionService,
    private excelImportService: ExcelImportService,
    private notificationService: NotificationService
  ) {
    this.bulkTransactionForm = this.fb.group({
      transactions: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadInitialData();
  }

  /**
   * Creates a mapping from display values (or extracted names) to dropdown keys
   * Handles case-insensitive matching and extracts meaningful names from composite values
   */
  private initializeAccountMappings(): void {
    this.initializeMapping(this.fromList, this.accountValueToKeyMap);
    this.initializeMapping(this.toList, this.accountValueToKeyMap);
    this.initializeMapping(this.transactionTagList, this.tagValueToKeyMap);
    this.initializeMapping(this.budgetAccountsList, this.budgetAccountValueToKeyMap);
  }

  /**
   * Initialize mapping for a dropdown list
   * Extracts meaningful names and creates case-insensitive lookup
   */
  private initializeMapping(dropdownList: DropDownDto[], mappingCache: Map<string, number>): void {
    dropdownList.forEach(item => {
      if (!item.value) return;

      // For account values like "ICICI/9000/SavingAccount", extract "ICICI"
      // For simple values like "Savings", use as-is
      const extractedName = this.extractMeaningfulName(item.value);

      // Store both the extracted name and the full value (case-insensitive)
      mappingCache.set(extractedName.toLowerCase(), item.key);
      mappingCache.set(item.value.toLowerCase(), item.key);

      // Also store with codeValue1 if available
      if (item.codeValue1) {
        mappingCache.set(item.codeValue1.toLowerCase(), item.key);
      }
    });
  }

  /**
   * Extract meaningful name from composite values
   * Example: "ICICI/9000/SavingAccount" -> "icici"
   * Example: "Salary" -> "salary"
   */
  private extractMeaningfulName(value: string): string {
    if (!value) return '';

    // If the value contains slashes, take the first part (typically the account/tag name)
    if (value.includes('/')) {
      return value.split('/')[0].trim();
    }

    return value.trim();
  }

  /**
   * Map an Excel value (display name) to the corresponding dropdown key
   * Returns the key if found, otherwise returns an empty string
   */
  private mapValueToKey(excelValue: string, mappingCache: Map<string, number>): number | null {
    if (!excelValue) return null;

    // First try exact case-insensitive match
    const lowerValue = excelValue.toLowerCase().trim();
    return mappingCache.get(lowerValue) || null;
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

      // Initialize mapping for Excel value translation
      this.initializeAccountMappings();
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

    // Store existing values before resetting
    const fromAccountValue = fromAccountControl?.value;
    const toAccountValue = toAccountControl?.value;

    this.resetControl(transactionGroup);

    // Restore the values after reset
    fromAccountControl?.setValue(fromAccountValue);
    toAccountControl?.setValue(toAccountValue);

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

    // Clear validators before resetting
    fromAccountControl?.clearValidators();
    toAccountControl?.clearValidators();

    // Reset controls but preserve marking
    fromAccountControl?.reset(fromAccountControl.value, { emitEvent: false });
    toAccountControl?.reset(toAccountControl.value, { emitEvent: false });
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

  async onExcelFileSelected(event: any): Promise<void> {
    const file: File = event.target.files[0];
    if (!file) {
      return;
    }

    if (!this.isValidExcelFile(file)) {
      this.notificationService.show('Please select a valid Excel file (.xlsx or .xls)', 'error');
      return;
    }

    this.isExcelImporting = true;

    try {
      const result = await this.excelImportService.parseExcelFile(file);

      if (!result.success || result.errors.length > 0) {
        result.errors.forEach(error => {
          this.notificationService.show(error, 'error');
        });
        this.isExcelImporting = false;
        return;
      }

      // Clear existing transactions
      this.transactions.clear();

      // Populate form from parsed data
      result.transactions.forEach((transaction: ParsedTransaction) => {
        this.addTransactionFromExcel(transaction);
      });

      this.notificationService.show(
        `Successfully imported ${result.transactions.length} transaction(s)`,
        'success'
      );
    } catch (error: any) {
      this.notificationService.show(
        `Error importing Excel file: ${error.message}`,
        'error'
      );
    } finally {
      this.isExcelImporting = false;
      // Reset file input
      event.target.value = '';
    }
  }

  private isValidExcelFile(file: File): boolean {
    const validMimeTypes = [
      'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      'application/vnd.ms-excel'
    ];
    const validExtensions = ['.xlsx', '.xls'];

    const hasValidMimeType = validMimeTypes.includes(file.type);
    const hasValidExtension = validExtensions.some(ext => file.name.toLowerCase().endsWith(ext));

    return hasValidMimeType || hasValidExtension;
  }

  private addTransactionFromExcel(transaction: ParsedTransaction): void {
    const budgetEntriesArray = this.fb.array([]);

    // Map Excel budget account names to keys and add budget entries
    transaction.budgetEntries.forEach(budgetEntry => {
      const mappedBudgetAccountId = this.mapValueToKey(
        budgetEntry.budgetAccountId,
        this.budgetAccountValueToKeyMap
      );

      // Validate that the budget account was successfully mapped
      if (mappedBudgetAccountId === null) {
        this.notificationService.show(
          `Budget account "${budgetEntry.budgetAccountId}" not found in the list. Please check the Excel file.`,
          'error'
        );
        return;
      }

      const budgetGroup = this.fb.group({
        budgetAccountId: [mappedBudgetAccountId, Validators.required],
        budgetAmount: [budgetEntry.budgetAmount, [Validators.required, Validators.min(0.01)]]
      });
      budgetEntriesArray.push(budgetGroup as any);
    });

    // If no budget entries, add at least one empty one
    if (budgetEntriesArray.length === 0) {
      const emptyBudgetGroup = this.fb.group({
        budgetAccountId: ['', Validators.required],
        budgetAmount: ['', [Validators.required, Validators.min(0.01)]]
      });
      budgetEntriesArray.push(emptyBudgetGroup as any);
    }

    // Map Excel account and tag names to dropdown keys
    const mappedFromAccountId = this.mapValueToKey(
      transaction.fromAccountId,
      this.accountValueToKeyMap
    );
    const mappedToAccountId = this.mapValueToKey(
      transaction.toAccountId,
      this.accountValueToKeyMap
    );
    const mappedTransactionTag = this.mapValueToKey(
      transaction.transactionTag,
      this.tagValueToKeyMap
    );

    // Validate mapped values
    const validationErrors: string[] = [];

    if (transaction.fromAccountId && mappedFromAccountId === null) {
      validationErrors.push(`From Account "${transaction.fromAccountId}" not found`);
    }
    if (transaction.toAccountId && mappedToAccountId === null) {
      validationErrors.push(`To Account "${transaction.toAccountId}" not found`);
    }
    if (mappedTransactionTag === null) {
      validationErrors.push(`Transaction Tag "${transaction.transactionTag}" not found`);
    }

    if (validationErrors.length > 0) {
      validationErrors.forEach(error => {
        this.notificationService.show(error + '. Please check the Excel file.', 'error');
      });
      return;
    }

    const transactionGroup = this.fb.group({
      transactionCategory: [transaction.transactionCategory, Validators.required],
      fromAccountId: [{ value: mappedFromAccountId ?? '', disabled: true }],
      toAccountId: [{ value: mappedToAccountId ?? '', disabled: true }],
      amount: [transaction.amount, [Validators.required, Validators.min(0.01)]],
      description: [transaction.description, Validators.required],
      transactionDate: [transaction.transactionDate, Validators.required],
      transactionTag: [transaction.transactionTag, Validators.required],
      budgetEntries: budgetEntriesArray
    });

    // Listen to transaction category changes to enable/disable account fields
    transactionGroup.get('transactionCategory')?.valueChanges.subscribe((category) => {
      this.updateAccountFieldsVisibility(transactionGroup, category ?? "");
    });

    (this.transactions as any).push(transactionGroup);

    // Update field visibility based on category
    this.updateAccountFieldsVisibility(transactionGroup, transaction.transactionCategory);
  }
}

