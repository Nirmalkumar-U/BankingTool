import { RequestId } from "../request-id";

export interface DepositWalletAmountRequestObject {
  request: DepositWalletAmountRequest;
}

export interface DepositWalletAmountRequest {
  wallet: DepositWalletAmountRequestWallet;
  walletTransaction: DepositWalletAmountRequestWalletTransaction;
  budgetAccount: DepositWalletAmountRequestBudgetAccount[];
}

export interface DepositWalletAmountRequestWallet extends RequestId {

}
export interface DepositWalletAmountRequestBudgetAccount extends RequestId {
  amount: number;
}

export interface DepositWalletAmountRequestWalletTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
}

export const CreateDepositWalletAmountRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date, budgetAccount: DepositWalletAmountRequestBudgetAccount[]): DepositWalletAmountRequestObject => {
  let model: DepositWalletAmountRequestObject = {
    request: {
      wallet: {
        id: accountId,
      },
      walletTransaction: {
        amount: amount,
        description: description,
        transactionDate: transactionDate
      },
      budgetAccount: budgetAccount
    }
  }
  return model;
}
