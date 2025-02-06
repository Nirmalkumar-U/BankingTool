import { DropDownDto } from "./drop-down-dto";

export interface CreateAccountInitialLoadDto {
  customerList: DropDownDto[]
  accountTypeList: DropDownDto[]
  bankList: DropDownDto[]
}
