import { isNullOrEmpty } from "../../../core/commonFunction/common-function";
import { RequestNullableId } from "../request-id";

export interface TransferAmountRequestObject {
  request: TransferAmountRequest;
}

export interface TransferAmountRequest {
  account: TransferAmountRequestAccount;
  transaction: TransferAmountRequestTransaction;
  budgetAccount: TransferAmountRequestBudgetAccount;
}

export interface TransferAmountRequestAccount {
  fromAccountId: number;
  toAccountId: number;
}

export interface TransferAmountRequestTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
  transactionTag: string;
}

export interface TransferAmountRequestBudgetAccount extends RequestNullableId {

}

export const getTransferAmountRequestObject = (fromAccountId: number, toAccountId: number, amount: number, description: string,
  transactionDate: Date, transactionTag: string, budgetAccountId: number | null): TransferAmountRequestObject => {
  let model: TransferAmountRequestObject = {
    request: {
      account: {
        toAccountId: toAccountId,
        fromAccountId: fromAccountId,
      },
      transaction: {
        amount: amount,
        description: description,
        transactionDate: transactionDate,
        transactionTag: transactionTag
      },
      budgetAccount: {
        id: isNullOrEmpty(budgetAccountId) ? null : budgetAccountId
      }
    }
  }
  return model;
}
