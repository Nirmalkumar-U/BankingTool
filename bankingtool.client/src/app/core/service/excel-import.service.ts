import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';

export interface ParsedTransaction {
  transactionNo: string;
  transactionCategory: string;
  fromAccountId: string;
  toAccountId: string;
  amount: number;
  description: string;
  transactionDate: string;
  transactionTag: string;
  budgetEntries: ParsedBudgetEntry[];
}

export interface ParsedBudgetEntry {
  budgetAccountId: string;
  budgetAmount: number;
}

export interface ExcelParseResult {
  success: boolean;
  transactions: ParsedTransaction[];
  errors: string[];
}

@Injectable({
  providedIn: 'root'
})
export class ExcelImportService {

  /**
   * Parse Excel file and extract transaction and budget data
   * Expected structure:
   * - Sheet 1: Transaction table with columns (TransactionNo, Category, FromAccount, ToAccount, Amount, Description, Date, Tag)
   * - Sheet 2: Budget table with columns (TransactionNo, BudgetAccount, Amount)
   */
  parseExcelFile(file: File): Promise<ExcelParseResult> {
    return new Promise((resolve) => {
      const reader = new FileReader();

      reader.onload = (event: any) => {
        try {
          const data = event.target.result;
          const workbook = XLSX.read(data, { type: 'array' });

          if (workbook.SheetNames.length < 2) {
            resolve({
              success: false,
              transactions: [],
              errors: ['Excel file must contain at least 2 sheets (Transactions and Budgets)']
            });
            return;
          }

          // Parse transaction table (first sheet)
          const transactionSheet = workbook.Sheets[workbook.SheetNames[0]];
          const transactions = this.parseTransactionTable(transactionSheet);

          if (transactions.errors.length > 0) {
            resolve({
              success: false,
              transactions: [],
              errors: transactions.errors
            });
            return;
          }

          // Parse budget table (second sheet)
          const budgetSheet = workbook.Sheets[workbook.SheetNames[1]];
          const budgets = this.parseBudgetTable(budgetSheet);

          if (budgets.errors.length > 0) {
            resolve({
              success: false,
              transactions: [],
              errors: budgets.errors
            });
            return;
          }

          // Map budgets to transactions
          const mappedTransactions = this.mapBudgetsToTransactions(transactions.data, budgets.data);

          resolve({
            success: true,
            transactions: mappedTransactions,
            errors: []
          });
        } catch (error: any) {
          resolve({
            success: false,
            transactions: [],
            errors: [`Error parsing Excel file: ${error.message}`]
          });
        }
      };

      reader.onerror = () => {
        resolve({
          success: false,
          transactions: [],
          errors: ['Error reading file']
        });
      };

      reader.readAsArrayBuffer(file);
    });
  }

  private parseTransactionTable(sheet: XLSX.WorkSheet): { data: ParsedTransaction[]; errors: string[] } {
    try {
      const rows = XLSX.utils.sheet_to_json(sheet);

      if (rows.length === 0) {
        return {
          data: [],
          errors: ['Transaction table is empty']
        };
      }

      const requiredColumns = ['TransactionNo', 'Category', 'FromAccount', 'Amount', 'Description', 'Date', 'Tag'];
      const firstRow = rows[0] as any;
      const missingColumns = requiredColumns.filter(col => !(col in firstRow));

      if (missingColumns.length > 0) {
        return {
          data: [],
          errors: [`Missing required columns in transaction table: ${missingColumns.join(', ')}`]
        };
      }

      const transactions: ParsedTransaction[] = rows.map((row: any, index: number) => {
        const errors = this.validateTransactionRow(row, index + 2);
        if (errors.length > 0) {
          throw new Error(errors.join('; '));
        }

        return {
          transactionNo: String(row['TransactionNo']).trim(),
          transactionCategory: String(row['Category']).trim(),
          fromAccountId: String(row['FromAccount']).trim(),
          toAccountId: row['ToAccount'] ? String(row['ToAccount']).trim() : '',
          amount: Number(row['Amount']),
          description: String(row['Description']).trim(),
          transactionDate: this.parseDate(row['Date']),
          transactionTag: String(row['Tag']).trim(),
          budgetEntries: []
        };
      });

      return { data: transactions, errors: [] };
    } catch (error: any) {
      return {
        data: [],
        errors: [`Error parsing transaction table: ${error.message}`]
      };
    }
  }

  private parseBudgetTable(sheet: XLSX.WorkSheet): { data: any[]; errors: string[] } {
    try {
      const rows = XLSX.utils.sheet_to_json(sheet);

      if (rows.length === 0) {
        return {
          data: [],
          errors: [] // Budget table can be empty
        };
      }

      const requiredColumns = ['TransactionNo', 'BudgetAccount', 'Amount'];
      const firstRow = rows[0] as any;
      const missingColumns = requiredColumns.filter(col => !(col in firstRow));

      if (missingColumns.length > 0) {
        return {
          data: [],
          errors: [`Missing required columns in budget table: ${missingColumns.join(', ')}`]
        };
      }

      const budgets = rows.map((row: any, index: number) => {
        const errors = this.validateBudgetRow(row, index + 2);
        if (errors.length > 0) {
          throw new Error(errors.join('; '));
        }

        return {
          transactionNo: String(row['TransactionNo']).trim(),
          budgetAccountId: String(row['BudgetAccount']).trim(),
          budgetAmount: Number(row['Amount'])
        };
      });

      return { data: budgets, errors: [] };
    } catch (error: any) {
      return {
        data: [],
        errors: [`Error parsing budget table: ${error.message}`]
      };
    }
  }

  private mapBudgetsToTransactions(transactions: ParsedTransaction[], budgets: any[]): ParsedTransaction[] {
    return transactions.map(transaction => {
      const transactionBudgets = budgets.filter(b => b.transactionNo === transaction.transactionNo);
      transaction.budgetEntries = transactionBudgets.map(b => ({
        budgetAccountId: b.budgetAccountId,
        budgetAmount: b.budgetAmount
      }));
      return transaction;
    });
  }

  private validateTransactionRow(row: any, rowNumber: number): string[] {
    const errors: string[] = [];

    if (!row['TransactionNo'] || String(row['TransactionNo']).trim() === '') {
      errors.push(`Row ${rowNumber}: TransactionNo is required`);
    }

    if (!row['Category'] || String(row['Category']).trim() === '') {
      errors.push(`Row ${rowNumber}: Category is required`);
    } else {
      const validCategories = ['Transfer', 'Deposit', 'Withdraw'];
      if (!validCategories.includes(String(row['Category']).trim())) {
        errors.push(`Row ${rowNumber}: Category must be one of: ${validCategories.join(', ')}`);
      }
    }

    if (!row['FromAccount'] || String(row['FromAccount']).trim() === '') {
      errors.push(`Row ${rowNumber}: FromAccount is required`);
    }

    //if (!row['ToAccount'] || String(row['ToAccount']).trim() === '') {
    //  errors.push(`Row ${rowNumber}: ToAccount is required`);
    //}

    if (!row['Amount'] || Number(row['Amount']) <= 0) {
      errors.push(`Row ${rowNumber}: Amount must be greater than 0`);
    }

    if (!row['Description'] || String(row['Description']).trim() === '') {
      errors.push(`Row ${rowNumber}: Description is required`);
    }

    if (!row['Date'] || String(row['Date']).trim() === '') {
      errors.push(`Row ${rowNumber}: Date is required`);
    }

    if (!row['Tag'] || String(row['Tag']).trim() === '') {
      errors.push(`Row ${rowNumber}: Tag is required`);
    }

    return errors;
  }

  private validateBudgetRow(row: any, rowNumber: number): string[] {
    const errors: string[] = [];

    if (!row['TransactionNo'] || String(row['TransactionNo']).trim() === '') {
      errors.push(`Row ${rowNumber}: TransactionNo is required`);
    }

    if (!row['BudgetAccount'] || String(row['BudgetAccount']).trim() === '') {
      errors.push(`Row ${rowNumber}: BudgetAccount is required`);
    }

    if (!row['Amount'] || Number(row['Amount']) <= 0) {
      errors.push(`Row ${rowNumber}: Amount must be greater than 0`);
    }

    return errors;
  }

  private parseDate(dateValue: any): string {
    if (!dateValue) {
      return new Date().toISOString().split('T')[0];
    }

    // Handle Excel date serial numbers
    if (typeof dateValue === 'number') {
      const excelEpoch = new Date(1900, 0, 1);
      const date = new Date(excelEpoch.getTime() + (dateValue - 1) * 24 * 60 * 60 * 1000);
      return date.toISOString().split('T')[0];
    }

    // Handle string dates
    if (typeof dateValue === 'string') {
      const date = new Date(dateValue);
      if (!isNaN(date.getTime())) {
        return date.toISOString().split('T')[0];
      }
    }

    return new Date().toISOString().split('T')[0];
  }
}
