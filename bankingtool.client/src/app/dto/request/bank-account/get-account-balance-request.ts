import { RequestId } from "../request-id";

export interface GetAccountBalanceRequestObject {
  request: GetAccountBalanceRequest;
}

export interface GetAccountBalanceRequest {
  account: GetAccountBalanceRequestAccount;
}

export interface GetAccountBalanceRequestAccount extends RequestId {

}
