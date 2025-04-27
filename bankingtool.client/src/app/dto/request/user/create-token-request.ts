import { RequestId } from "../request-id";

export interface CreateTokenRequestObject {
  request: CreateTokenRequest;
}

export interface CreateTokenRequest {
  user: CreateTokenRequestUser;
  role: CreateTokenRequestRole;
}

export interface CreateTokenRequestUser extends RequestId {
  firstName: string;
  lastName: string;
  email: string;
}

export interface CreateTokenRequestRole extends RequestId {

}
