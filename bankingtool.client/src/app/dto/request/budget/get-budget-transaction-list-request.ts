import { RequestId } from "../request-id";

export interface GetBudgetTransactionListRequestObject {
  request: GetBudgetTransactionListRequest;
}

export interface GetBudgetTransactionListRequest {
  budgetAccount: GetBudgetTransactionListRequestBudgetAccount;
}

export interface GetBudgetTransactionListRequestBudgetAccount extends RequestId {
}

export const CreateGetBudgetTransactionListRequestObject = (budgetAccountId: number): GetBudgetTransactionListRequestObject => {
  let model: GetBudgetTransactionListRequestObject = {
    request: {
      budgetAccount: {
        id: budgetAccountId
      }
    }
  }
  return model;
}
