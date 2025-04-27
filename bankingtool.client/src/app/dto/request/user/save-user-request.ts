import { RequestId } from "../request-id";

export interface SaveUserRequestObject {
  request: SaveUserRequest;
}

export interface SaveUserRequest {
  user: SaveUserRequestUser;
  state: SaveUserRequestState;
  city: SaveUserRequestCity;
  role: SaveUserRequestRole;
}

export interface SaveUserRequestUser {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export interface SaveUserRequestState extends RequestId {

}

export interface SaveUserRequestCity extends RequestId {

}

export interface SaveUserRequestRole extends RequestId {

}
