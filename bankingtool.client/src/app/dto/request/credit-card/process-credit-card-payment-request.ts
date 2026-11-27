import { RequestId } from '../request-id';

export interface ProcessCreditCardPaymentRequestObject {
  request: ProcessCreditCardPaymentRequest;
}

export interface ProcessCreditCardPaymentRequest {
  creditCard: RequestId;
  transaction: ProcessCreditCardPaymentRequestTransaction;
}

export interface ProcessCreditCardPaymentRequestTransaction {
  paymentAmount: number;
  description: string;
  transactionDate: Date;
  fees: number;
}

export const CreateProcessCreditCardPaymentRequestObject = (creditCardId: number, paymentAmount: number, description:string, transactionDate:Date, fees: number): ProcessCreditCardPaymentRequestObject => {
  let model: ProcessCreditCardPaymentRequestObject = {
    request: {
      creditCard: {
        id: creditCardId
      },
      transaction: {
        paymentAmount: paymentAmount,
        description: description,
        transactionDate: transactionDate, 
        fees: fees
      }
    }
  }
  return model;
}
