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

export const createTransactionListRequestObject = (accountId: number, receiverAccountId: number | null, senderAccountId: number | null,
  transactionCategory: string | null, transactionFromDate: Date | null, transactionTag: string | null, transactionToDate: Date | null): TransactionListRequestObject => {
  let model: TransactionListRequestObject = {
    request: {
      account: {
        id: accountId,
        receiverAccountId: receiverAccountId,
        senderAccountId: senderAccountId
      },
      transaction: {
        transactionCategory: transactionCategory,
        transactionFromDate: transactionFromDate,
        transactionTag: transactionTag,
        transactionToDate: transactionToDate
      }
    }
  }
  return model;
}
