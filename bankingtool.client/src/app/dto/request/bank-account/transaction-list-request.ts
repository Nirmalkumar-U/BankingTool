import { RequestId } from "../request-id";

export interface TransactionListRequestObject {
  request: TransactionListRequest;
}

export interface TransactionListRequest {
  account: TransactionListRequestAccount;
  transaction: TransactionListRequestTransaction;
}

export interface TransactionListRequestAccount extends RequestId {
  senderAccountId: number | null;
  receiverAccountId: number | null;
}

export interface TransactionListRequestTransaction {
  transactionTag: string | null;
  transactionFromDate: Date | null;
  transactionToDate: Date | null;
  transactionCategory: string | null
}
