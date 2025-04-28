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

export interface TransactionsListForCustomerRequestAccount {
  accountTypeId: number;
}

export interface TransactionsListForCustomerRequestCustomer extends RequestId {

}
