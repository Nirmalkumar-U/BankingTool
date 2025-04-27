import { RequestNullableId } from "../request-id";

export interface GetUserInitialLoadRequestObject {
  request: GetUserInitialLoadRequest;
}

export interface GetUserInitialLoadRequest {
  user: GetUserInitialLoadRequestUser;
}

export interface GetUserInitialLoadRequestUser extends RequestNullableId {

}
