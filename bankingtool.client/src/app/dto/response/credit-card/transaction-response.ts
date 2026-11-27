export interface TransactionResponseObject {
  response: TransactionResponse;
}

export interface TransactionResponseListObject {
  response: TransactionResponse[];
}

export interface TransactionResponse {
  transaction: TransactionResponseTransaction;
  creditCard: TransactionResponseCreditCard;
  account: TransactionResponseAccount;
}

export interface TransactionResponseCreditCard {
  id: number;
  creditCardNumber: string;
  cardDetail: string;
  cardholderName: string;
}

export interface TransactionResponseAccount {
  id: number;
}

export interface TransactionResponseTransaction {
  id: number;
  transactionDate: string;
  transactionType: string;
  amount: number;
  description: string;
  merchantName: string;
  transactionStatus: string;
  referenceNumber: string;
}
