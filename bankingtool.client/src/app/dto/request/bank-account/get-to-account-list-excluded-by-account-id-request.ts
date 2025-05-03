import { RequestId } from "../request-id";

export interface GetToAccountListExcludedByAccountIdRequestObject {
  request: GetToAccountListExcludedByAccountIdRequest;
}

export interface GetToAccountListExcludedByAccountIdRequest {
  account: GetToAccountListExcludedByAccountIdRequestAccount;
}

export interface GetToAccountListExcludedByAccountIdRequestAccount extends RequestId {

}

export const CreateGetToAccountListExcludedByAccountIdRequestObject = (accountId: number): GetToAccountListExcludedByAccountIdRequestObject => {
  let model: GetToAccountListExcludedByAccountIdRequestObject = {
    request: {
      account: {
        id: accountId,
      }
    }
  }
  return model;
}
