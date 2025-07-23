import { RequestId } from "../request-id";

export interface GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject {
  request: GetToBudgetDropdownListAndBalanceOfBudgetAccountRequest;
}

export interface GetToBudgetDropdownListAndBalanceOfBudgetAccountRequest {
  budgetAccount: GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestBudgetAccount;
}

export interface GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestBudgetAccount extends RequestId { }

export const CreateGetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject = (budgetAccountId: number):
  GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject => {
  let model: GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject = {
    request: {
      budgetAccount: {
        id: budgetAccountId
      }
    }
  }
  return model;
}
