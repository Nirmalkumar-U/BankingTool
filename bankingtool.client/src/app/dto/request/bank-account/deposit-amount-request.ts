import { RequestId } from "../request-id";

export interface DepositAmountRequestObject {
  request: DepositAmountRequest;
}

export interface DepositAmountRequest {
  account: DepositAmountRequestAccount;
  transaction: DepositAmountRequestTransaction;
  budgetAccount: DepositAmountRequestBudgetAccount[];
}

export interface DepositAmountRequestAccount extends RequestId {

}
export interface DepositAmountRequestBudgetAccount extends RequestId {
  amount: number;
}

export interface DepositAmountRequestTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
}

export const CreateDepositAmountRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date, budgetAccount: DepositAmountRequestBudgetAccount[]): DepositAmountRequestObject => {
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
      budgetAccount: budgetAccount
    }
  }
  return model;
}
