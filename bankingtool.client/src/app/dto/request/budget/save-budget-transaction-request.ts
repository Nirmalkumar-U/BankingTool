import { RequestId } from "../request-id";

export interface SaveBudgetTransactionRequestObject {
  request: SaveBudgetTransactionRequest;
}

export interface SaveBudgetTransactionRequest {
  fromBudgetAccount: SaveBudgetTransactionRequestFromBudgetAccount;
  toBudgetAccount: SaveBudgetTransactionRequestToBudgetAccount[];
}

export interface SaveBudgetTransactionRequestFromBudgetAccount extends RequestId {
  amount: number;
  description: string;
}

export interface SaveBudgetTransactionRequestToBudgetAccount extends RequestId {
  amount: number;
}

export const CreateSaveBudgetTransactionRequestObject = (fromBudgetAccountId: number, amount: number, description: string, toBudgetAccount: SaveBudgetTransactionRequestToBudgetAccount[]):
  SaveBudgetTransactionRequestObject => {
  let model: SaveBudgetTransactionRequestObject = {
    request: {
      fromBudgetAccount: {
        id: fromBudgetAccountId,
        amount: amount,
        description: description
      },
      toBudgetAccount: toBudgetAccount
    }
  }
  return model;
}
