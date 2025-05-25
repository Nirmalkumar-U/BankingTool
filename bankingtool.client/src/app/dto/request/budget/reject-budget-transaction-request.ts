import { RequestId } from "../request-id";

export interface RejectBudgetTransactionRequestObject {
  request: RejectBudgetTransactionRequest;
}

export interface RejectBudgetTransactionRequest {
  budgetNotification: RejectBudgetTransactionRequestBudgetNotification;
}

export interface RejectBudgetTransactionRequestBudgetNotification extends RequestId {

}

export const CreateRejectBudgetTransactionRequestObject = (budgetNotificationId: number): RejectBudgetTransactionRequestObject => {
  let model: RejectBudgetTransactionRequestObject = {
    request: {
      budgetNotification: {
        id: budgetNotificationId
      }
    }
  }
  return model;
}
