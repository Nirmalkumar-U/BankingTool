import { RequestId } from "../request-id";

export interface GetCityListRequestObject {
  request: GetCityListRequest;
}

export interface GetCityListRequest {
  state: GetCityListRequestState;
}

export interface GetCityListRequestState extends RequestId {

}
