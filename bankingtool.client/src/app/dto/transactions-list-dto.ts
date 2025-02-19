export interface TransactionsListDto {
    transactionId: number;
    transactionDate: Date;
    amount: number;
    transactionType: string;
    stageBalance: number;
}
