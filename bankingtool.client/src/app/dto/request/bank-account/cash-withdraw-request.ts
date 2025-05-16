import { isNullOrEmpty } from "../../../core/commonFunction/common-function";
import { RequestId, RequestNullableId } from "../request-id";

export interface CashWithdrawRequestObject {
  request: CashWithdrawRequest;
}

export interface CashWithdrawRequest {
  account: CashWithdrawRequestAccount;
  transaction: CashWithdrawRequestTransaction;
  budgetAccount: CashWithdrawRequestBudgetAccount;
}

export interface CashWithdrawRequestAccount extends RequestId {

}
export interface CashWithdrawRequestBudgetAccount extends RequestNullableId {

}

export interface CashWithdrawRequestTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
}

export const CreateCashWithdrawRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date, budgetAccountId: number | null): CashWithdrawRequestObject => {
  let model: CashWithdrawRequestObject = {
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
