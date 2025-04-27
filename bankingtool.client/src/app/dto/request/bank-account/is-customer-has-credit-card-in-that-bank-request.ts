import { RequestId } from "../request-id";

export interface IsCustomerHasCreditCardInThatBankRequestObject {
  request: IsCustomerHasCreditCardInThatBankRequest;
}

export interface IsCustomerHasCreditCardInThatBankRequest {
  customer: IsCustomerHasCreditCardInThatBankRequestCustomer;
  bank: IsCustomerHasCreditCardInThatBankRequestBank;
}

export interface IsCustomerHasCreditCardInThatBankRequestCustomer extends RequestId {

}

export interface IsCustomerHasCreditCardInThatBankRequestBank extends RequestId {

}
