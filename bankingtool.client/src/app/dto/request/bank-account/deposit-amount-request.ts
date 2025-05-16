import { isNullOrEmpty } from "../../../core/commonFunction/common-function";
import { RequestId, RequestNullableId } from "../request-id";

export interface DepositAmountRequestObject {
  request: DepositAmountRequest;
}

export interface DepositAmountRequest {
  account: DepositAmountRequestAccount;
  transaction: DepositAmountRequestTransaction;
  budgetAccount: DepositAmountRequestBudgetAccount;
}

export interface DepositAmountRequestAccount extends RequestId {

}
export interface DepositAmountRequestBudgetAccount extends RequestNullableId {

}

export interface DepositAmountRequestTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
}

export const CreateDepositAmountRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date, budgetAccountId: number | null): DepositAmountRequestObject => {
  let model: DepositAmountRequestObject = {
    request: {
      account: {
        id: accountId,
      },
      transaction: {
        amount: amount,
        description: description,
        transactionDate: transactionDate
      },
      budgetAccount: {
        id: isNullOrEmpty(budgetAccountId) ? null : budgetAccountId 
      }
    }
  }
  return model;
}
