import { RequestId } from "../request-id";

export interface GetWalletBalanceRequestObject {
  request: GetWalletBalanceRequest;
}

export interface GetWalletBalanceRequest {
  account: GetWalletBalanceRequestAccount;
}

export interface GetWalletBalanceRequestAccount extends RequestId {

}
