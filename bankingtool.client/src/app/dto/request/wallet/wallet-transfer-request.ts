import { isNullOrEmpty } from "../../../core/commonFunction/common-function";
import { RequestId, RequestNullableId } from "../request-id";
export interface WalletTransferRequestObject {
  request: WalletTransferRequest;
}

export interface WalletTransferRequest {
  wallet: WalletTransferRequestWallet;
  walletTransaction: WalletTransferRequestWalletTransaction;
  budgetAccount: WalletTransferRequestBudgetAccount[];
  account: WalletTransferRequestAccount;
}

export interface WalletTransferRequestWallet extends RequestId {

}

export interface WalletTransferRequestWalletTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
  transactionTag: string;
}

export interface WalletTransferRequestBudgetAccount extends RequestId {
  amount: number;
}

export interface WalletTransferRequestAccount extends RequestNullableId {

}

export const CreateWalletTransferRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date,
  budgetAccount: WalletTransferRequestBudgetAccount[], bankAccountId: number | null, transactionTag: string): WalletTransferRequestObject => {
  let model: WalletTransferRequestObject = {
    request: {
      wallet: {
        id: accountId,
      },
      walletTransaction: {
        amount: amount,
        description: description,
        transactionDate: transactionDate,
        transactionTag: transactionTag
      },
      budgetAccount: budgetAccount,
      account: {
        id: isNullOrEmpty(bankAccountId) ? null : bankAccountId,
      }
    }
  }
  return model;
}
