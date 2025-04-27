import { RequestId } from "../request-id";

export interface BankDropDownListRequestObject {
  request: BankDropDownListRequest;
}

export interface BankDropDownListRequest {
  customer: BankDropDownListRequestCustomer;
}

export interface BankDropDownListRequestCustomer extends RequestId {

}
