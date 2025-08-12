export interface GetWalletTransactionListResponse {
  walletName: string;
  walletNumber: string;
  walletStatus: string;
  balance: number;
  walletTransactionList: GetWalletTransactionList[];
}

export interface GetWalletTransactionList {
  transactionTag: string;
  transactionDate: string;
  description: string;
  receiverAmount: number | null;
  senderAmount: number | null;
  stageBalance: number;
}
