export interface GetTransactionsListResponse {
  transactionsList: GetTransactionsListResponseTransactionList[];
  accountInfo: GetTransactionsListResponseAccountInfo;
  cardInfo: GetTransactionsListResponseCardInfo[];
}

export interface GetTransactionsListResponseTransactionList {
  transactionId: number;
  transactionDate: string;
  amount: number;
  transactionType: string;
  description: string;
  stageBalance: number;
}

export interface GetTransactionsListResponseAccountInfo {
  accountNumber: string;
  bankName: string;
  accountHolderName: string;
  balance: number;
  accountType: string;
}

export interface GetTransactionsListResponseCardInfo {
  cardType: string;
  cardNumber: string;
  bankName: string;
  holderName: string;
  expirationDate: string;
  cVV: number;
  balanceLimit: number | null;
}
