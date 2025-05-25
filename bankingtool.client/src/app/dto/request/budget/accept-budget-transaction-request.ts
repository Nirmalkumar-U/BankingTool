import { RequestId } from "../request-id";

export interface AcceptBudgetTransactionRequestObject {
  request: AcceptBudgetTransactionRequest;
}

export interface AcceptBudgetTransactionRequest {
  budgetAccount: AcceptBudgetTransactionRequestBudgetAcccount;
  budgetNotification: AcceptBudgetTransactionRequestBudgetNotification;
}

export interface AcceptBudgetTransactionRequestBudgetAcccount extends RequestId {

}

export interface AcceptBudgetTransactionRequestBudgetNotification extends RequestId {

}

export const CreateAcceptBudgetTransactionRequestObject = (budgetAccountId: number, budgetNotificationId: number): AcceptBudgetTransactionRequestObject => {
  let model: AcceptBudgetTransactionRequestObject = {
    request: {
      budgetAccount: {
        id: budgetAccountId
      },
      budgetNotification: {
        id: budgetNotificationId
      }
    }
  }
  return model;
}
