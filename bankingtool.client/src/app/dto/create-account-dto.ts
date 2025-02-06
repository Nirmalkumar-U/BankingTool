export interface CreateAccountDto {
  bankId: number;
  accountTypeId: number;
  customerId: number;
  customerWantCreditCard: boolean;
  doYouWantToChangeThisAccountToPrimaryAccount: boolean;
}
