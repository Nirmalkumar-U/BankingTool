export interface GetTransactionsListResponse {
  transactionsList: GetTransactionsListResponseTransactionList[];
  accountInfo: GetTransactionsListResponseAccountInfo;
  cardInfo: GetTransactionsListResponseCardInfo[];
}

export interface GetTransactionsListResponseTransactionList {
  transactionId: number;
  amount: number;
  stageBalance: number;
  transactionDate: Date;
  description: string;
  transactionType: string;
  transactionCategory: string;
  fromAccountId: string;
  toAccountId: string;
}

export interface GetTransactionsListResponseAccountInfo {
  accountNumber: string;
  bankName: string;
  accountHolderName: string;
  balance: number;
  accountType: string;
  accountId: number;
}

export interface GetTransactionsListResponseCardInfo {
  cardType: string;
  cardNumber: string;
  bankName: string;
  holderName: string;
  expirationDate: string;
  cvv: number;
  balanceLimit: number | null;
}
