import { RequestId } from "../request-id";

export interface RecordCreditCardTransactionRequestObject {
  request: RecordCreditCardTransactionRequest;
}

export interface RecordCreditCardTransactionRequest {
  creditCard: RequestId;
  transaction: RecordCreditCardTransactionRequestTransaction;
  budgetAccounts: RecordCreditCardTransactionRequestBudgetAccount[];
}

export interface RecordCreditCardTransactionRequestBudgetAccount extends RequestId {
  amount: number;
}

export interface RecordCreditCardTransactionRequestTransaction {
  transactionType: string;
  amount: number;
  description: string;
  merchantName: string;
  transactionDate: Date;
}

export const CreateRecordCreditCardTransactionRequestObject = (creditCardId: number, amount: number, description:string,transactionDate:Date,
              merchantName:string,transactionType:string, budgetAccount: RecordCreditCardTransactionRequestBudgetAccount[]): RecordCreditCardTransactionRequestObject => {
  let model: RecordCreditCardTransactionRequestObject = {
    request: {
      creditCard: {
        id: creditCardId
      },
      budgetAccounts: budgetAccount,
      transaction: {
        amount:amount,
        description: description,
        transactionDate:transactionDate,
        merchantName: merchantName,
        transactionType:transactionType
      }
    }
  }
  return model;
}
