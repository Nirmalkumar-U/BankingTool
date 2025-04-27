import { RequestId } from "../request-id";

export interface TransactionsListForCustomerRequestObject {
  request: TransactionsListForCustomerRequest;
}

export interface TransactionsListForCustomerRequest {
  customer: TransactionsListForCustomerRequestCustomer;
  account: TransactionsListForCustomerRequestAccount;
  bank: TransactionsListForCustomerRequestBank;
}

export interface TransactionsListForCustomerRequestBank extends RequestId {

}

export interface TransactionsListForCustomerRequestAccount extends RequestId {

}

export interface TransactionsListForCustomerRequestCustomer extends RequestId {

}
