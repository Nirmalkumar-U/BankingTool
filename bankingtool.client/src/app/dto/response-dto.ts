export interface ResponseDto<T> {
  message: string[];
  result: T;
  status: boolean;
}
