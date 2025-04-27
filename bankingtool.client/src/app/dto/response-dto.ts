import { DropDownListDto } from "./drop-down-dto";

export interface ResponseDto<T> {
  dropDownList: DropDownListDto[];
  errors: Errors[];
  validationErrors: ValidationResults[];
  response: T;
  status: boolean;
  statuCode: number;
  message: string;
}

export interface Errors {
  errorMessage: string;
  propertyName: string;
}

export interface ValidationResults {
  propertyName: string;
  propertyPath: string;
  errorMessage: string;
}
