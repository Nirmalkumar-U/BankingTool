import { RequestId } from "../request-id";

export interface CreateAccountRequestObject {
  request: CreateAccountRequest;
}

export interface CreateAccountRequest {
  bank: CreateAccountRequestBank;
  account: CreateAccountRequestAccount;
  customer: CreateAccountRequestCustomer;
}

export interface CreateAccountRequestBank extends RequestId {

}

export interface CreateAccountRequestCustomer extends RequestId {
  customerWantCreditCard: boolean;
}

export interface CreateAccountRequestAccount {
  accountTypeId: number;
  doYouWantToChangeThisAccountToPrimaryAccount: boolean;
}
