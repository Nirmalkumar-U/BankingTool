import { RequestId } from "../request-id";

export interface GetBankDetailWithoutCustomerAndAccountTypeRequestObject {
  request: GetBankDetailWithoutCustomerAndAccountTypeRequest;
}

export interface GetBankDetailWithoutCustomerAndAccountTypeRequest {
  customer: GetBankDetailWithoutCustomerAndAccountTypeRequestCustomer;
  account: GetBankDetailWithoutCustomerAndAccountTypeRequestAccount;
}

export interface GetBankDetailWithoutCustomerAndAccountTypeRequestCustomer extends RequestId {

}

export interface GetBankDetailWithoutCustomerAndAccountTypeRequestAccount {
  accountTypeId: number;
}
