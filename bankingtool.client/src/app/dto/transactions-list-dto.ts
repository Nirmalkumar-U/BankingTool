export interface TransactionsListDto {
  transactionId: number;
  transactionDate: Date;
  amount: number;
  transactionType: string;
  description: string;
  stageBalance: number;
}
