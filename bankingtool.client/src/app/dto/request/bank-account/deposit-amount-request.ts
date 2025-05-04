import { RequestId } from "../request-id";

export interface DepositAmountRequestObject {
  request: DepositAmountRequest;
}

export interface DepositAmountRequest {
  account: DepositAmountRequestAccount;
  transaction: DepositAmountRequestTransaction;
}

export interface DepositAmountRequestAccount extends RequestId {

}

export interface DepositAmountRequestTransaction {
  amount: number;
  description: string;
}

export const CreateDepositAmountRequestObject = (accountId: number, amount: number, description: string): DepositAmountRequestObject => {
  let model: DepositAmountRequestObject = {
    request: {
      account: {
        id: accountId,
      },
      transaction: {
        amount: amount,
        description: description
      }
    }
  }
  return model;
}
