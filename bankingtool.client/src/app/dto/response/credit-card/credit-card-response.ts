import { CreditCardNumberResponse } from "./credit-card-number-response";

export interface CreditCardResponseObject {
  response: CreditCardResponse;
}

export interface CreditCardResponseListObject {
  responses: CreditCardResponse[];
}

export interface CreditCardResponse {
  creditCard: CreditCardResponseCreditCard;
  account: CreditCardResponseAccount;
}

export interface CreditCardResponseCreditCard {
  id: number;
  creditLimit: number;
  balanceCredit: number;
  availableCredit: number;
  status: string;
  cardHolderName: string;
  cardType: string;
  cardNumber: CreditCardNumberResponse;
  applicationDate: string;
  approvedDate?: string;
  expiryDate?: string;
  approvedLimit?: number;
  limitUsed?: number;
  currentLimit?: number;
}

export interface CreditCardResponseAccount {
  id: number;
  accountNumber: string;
  accountAbbreviation: string;
  accountDetail: string;
}
