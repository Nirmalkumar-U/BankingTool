import { DropDownDto } from "./drop-down-dto";
import { UserDetailDto } from "./user-detail-dto";

export interface UserInitialLoadDto {
  roleDropDown: DropDownDto[];
  stateDropDown: DropDownDto[];
  userDetail: UserDetailDto;
}
