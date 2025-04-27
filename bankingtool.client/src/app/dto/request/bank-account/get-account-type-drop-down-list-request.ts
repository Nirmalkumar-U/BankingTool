import { RequestId } from "../request-id";

export interface GetAccountTypeDropDownListRequestObject {
  request: GetAccountTypeDropDownListRequest;
}

export interface GetAccountTypeDropDownListRequest {
  customer: GetAccountTypeDropDownListRequestCustomer;
  bank: GetAccountTypeDropDownListRequestBank;
}

export interface GetAccountTypeDropDownListRequestCustomer extends RequestId {

}

export interface GetAccountTypeDropDownListRequestBank extends RequestId {

}
