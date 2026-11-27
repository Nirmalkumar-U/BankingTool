import { RequestId } from "../request-id";

export interface CreditCardApplicationRequestObject {
  request: CreditCardApplicationRequest;
}

export interface CreditCardApplicationRequest {
  account: RequestId;
  transaction: CreditCardApplicationRequestTransaction;
}

export interface CreditCardApplicationRequestTransaction {
  cardType: string;
  desiredLimit: number;
}

export const CreateCreditCardApplicationRequestObject = (accountId: number, cardType: string, desiredLimit: number): CreditCardApplicationRequestObject => {
  let model: CreditCardApplicationRequestObject = {
    request: {
      account: {
        id: accountId,
      },
      transaction: {
        cardType: cardType,
        desiredLimit: desiredLimit
      }
    }
  }
  return model;
}
