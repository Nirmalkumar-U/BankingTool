export interface LoginRequestObject {
  request: LoginRequest;
}

export interface LoginRequest {
  user: LoginRequestUser;
}

export interface LoginRequestUser {
  email: string;
  password: string;
}
