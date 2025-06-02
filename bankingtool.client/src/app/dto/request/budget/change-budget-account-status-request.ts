import { RequestId } from "../request-id";

export interface ChangeBudgetAccountStatusRequestObject {
  request: ChangeBudgetAccountStatusRequest;
}

export interface ChangeBudgetAccountStatusRequest {
  budgetAccount: ChangeBudgetAccountStatusRequestBudgetAccount;
}

export interface ChangeBudgetAccountStatusRequestBudgetAccount extends RequestId {

}

export const CreateChangeBudgetAccountStatusRequestObject = (budgetAccountId: number): ChangeBudgetAccountStatusRequestObject => {
  let model: ChangeBudgetAccountStatusRequestObject = {
    request: {
      budgetAccount: {
        id: budgetAccountId
      }
    }
  }
  return model;
}
