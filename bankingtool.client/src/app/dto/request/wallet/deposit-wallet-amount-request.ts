import { isNullOrEmpty } from "../../../core/commonFunction/common-function";
import { RequestId, RequestNullableId } from "../request-id";

export interface DepositWalletAmountRequestObject {
  request: DepositWalletAmountRequest;
}

export interface DepositWalletAmountRequest {
  wallet: DepositWalletAmountRequestWallet;
  walletTransaction: DepositWalletAmountRequestWalletTransaction;
  budgetAccount: DepositWalletAmountRequestBudgetAccount[];
  account: DepositWalletAmountRequestAccount;
}

export interface DepositWalletAmountRequestWallet extends RequestId {

}
export interface DepositWalletAmountRequestBudgetAccount extends RequestId {
  amount: number;
}
export interface DepositWalletAmountRequestAccount extends RequestNullableId {
  isFromAccount: boolean;
}
export interface DepositWalletAmountRequestWalletTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
}

export const CreateDepositWalletAmountRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date,
  budgetAccount: DepositWalletAmountRequestBudgetAccount[], isFromAccount: boolean, bankAccountId: number | null): DepositWalletAmountRequestObject => {
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
      budgetAccount: budgetAccount,
      account: {
        id: isNullOrEmpty(bankAccountId) ? null : bankAccountId,
        isFromAccount: isFromAccount
      }
    }
  }
  return model;
}
