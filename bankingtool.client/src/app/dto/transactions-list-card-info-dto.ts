export interface TransactionsListCardInfoDto {
  cardType: string;
  cardNumber: string;
  bankName: string;
  holderName: string;
  expirationDate: string;
  cvv: number;
  balanceLimit: number | null;
}
