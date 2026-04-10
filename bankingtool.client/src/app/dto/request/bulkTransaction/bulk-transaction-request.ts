import { RequestId } from "../request-id";

export interface BulkTransactionRequestObject {
  request: BulkTransactionRequest[];
}

export interface BulkTransactionRequest {
  transactionCategory: 'Transfer' | 'Withdraw' | 'Deposit';
  budgetAccount: BulkTransactionRequestBudgetAccount[];
  account: BulkTransactionRequestAccount;
  transaction: BulkTransactionRequestTransaction;
}

export interface BulkTransactionRequestAccount {
  fromAccountId: number;
  toAccountId: number;
}

export interface BulkTransactionRequestTransaction{
  amount: number;
  description: string;
  transactionDate: string;
  transactionTag: string;
}

export interface BulkTransactionRequestBudgetAccount extends RequestId {
  amount: number;
}
