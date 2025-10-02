export interface DropDownDto {
  key: number;
  value: string;
  isSelected: boolean;
  codeValue1: string | null;
}

export interface DropDownListDto {
  name: string;
  dropDown: DropDownDto[];
}

export interface YesOrNoDto {
  key: boolean;
  value: string;
}
