export interface UserInitialLoadResponse {
  userDetail: UserDetailResponse;
}

export interface UserDetailResponse {
  userId: number;
  password: string;
  emailId: string;
  firstName: string;
  lastName: string;
  city: number;
  state: number;
  roleId: number;
}
