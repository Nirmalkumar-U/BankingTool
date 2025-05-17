export interface GetBudgetTransactionListResponse {
  budgetName: string;
  budgetNumber: string;
  budgetStatus: string;
  balance: number;
  budgetTransactionList: GetBudgetTransactionList[];
}

export interface GetBudgetTransactionList {
  accountName: string;
  transactionTag: string;
  transactionDate: string;
  description: string;
  receiverAmount: number | null;
  senderAmount: number | null;
  stageBalance: number;
}
