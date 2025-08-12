import { RequestId } from "../request-id";

export interface GetWalletTransactionListRequestObject {
  request: GetWalletTransactionListRequest;
}

export interface GetWalletTransactionListRequest {
  wallet: GetWalletTransactionListRequestWallet;
}

export interface GetWalletTransactionListRequestWallet extends RequestId {
}

export const CreateGetWalletTransactionListRequestObject = (walletId: number): GetWalletTransactionListRequestObject => {
  let model: GetWalletTransactionListRequestObject = {
    request: {
      wallet: {
        id: walletId
      }
    }
  }
  return model;
}
