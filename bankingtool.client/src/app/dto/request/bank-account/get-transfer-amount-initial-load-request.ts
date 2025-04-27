import { RequestId } from "../request-id";

export interface GetTransferAmountInitialLoadRequestObject {
  request: GetTransferAmountInitialLoadRequest;
}

export interface GetTransferAmountInitialLoadRequest {
  customer: GetTransferAmountInitialLoadRequestCustomer;
}

export interface GetTransferAmountInitialLoadRequestCustomer extends RequestId {

}
