import { formatDate } from "@angular/common";

export const isNullOrEmpty = (value: string | number | null | undefined): boolean => {
  return (value == 'undefined' || value == undefined || value == null || value == "null" || value == "" || (typeof value === "string" && value.length == 0));
}

export const isAllEmptyOrNull = (value: any) => { return Object.values(value).every(value => value === '' || value === null); }

export const formatDateForInput = (date: Date): string => {
  return formatDate(date, 'yyyy-MM-dd', 'en');
}
