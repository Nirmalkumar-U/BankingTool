import { RequestId } from "../request-id";

export interface AcceptBudgetTransactionRequestObject {
  request: AcceptBudgetTransactionRequest;
}

export interface AcceptBudgetTransactionRequest {
  budgetAccount: AcceptBudgetTransactionRequestBudgetAcccount[];
  budgetNotification: AcceptBudgetTransactionRequestBudgetNotification;
}

export interface AcceptBudgetTransactionRequestBudgetAcccount extends RequestId {
  amount: number;
}

export interface AcceptBudgetTransactionRequestBudgetNotification extends RequestId {

}

export const CreateAcceptBudgetTransactionRequestObject = (budgetAccount: AcceptBudgetTransactionRequestBudgetAcccount[], budgetNotificationId: number): AcceptBudgetTransactionRequestObject => {
  let model: AcceptBudgetTransactionRequestObject = {
    request: {
      budgetAccount: budgetAccount,
      budgetNotification: {
        id: budgetNotificationId
      }
    }
  }
  return model;
}
