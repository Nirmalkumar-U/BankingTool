import { RequestId } from "../request-id";

export interface ChangeWalletStatusRequestObject {
  request: ChangeWalletStatusRequest;
}

export interface ChangeWalletStatusRequest {
  wallet: ChangeWalletStatusRequestWallet;
}

export interface ChangeWalletStatusRequestWallet extends RequestId {

}

export const CreateChangeWalletStatusRequestObject = (WalletId: number): ChangeWalletStatusRequestObject => {
  let model: ChangeWalletStatusRequestObject = {
    request: {
      wallet: {
        id: WalletId
      }
    }
  }
  return model;
}
