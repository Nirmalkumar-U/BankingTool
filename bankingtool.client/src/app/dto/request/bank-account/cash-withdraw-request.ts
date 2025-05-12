import { RequestId } from "../request-id";

export interface CashWithdrawRequestObject {
  request: CashWithdrawRequest;
}

export interface CashWithdrawRequest {
  account: CashWithdrawRequestAccount;
  transaction: CashWithdrawRequestTransaction;
}

export interface CashWithdrawRequestAccount extends RequestId {

}

export interface CashWithdrawRequestTransaction {
  amount: number;
  description: string;
  transactionDate: Date;
}

export const CreateCashWithdrawRequestObject = (accountId: number, amount: number, description: string, transactionDate: Date): CashWithdrawRequestObject => {
  let model: CashWithdrawRequestObject = {
    request: {
      account: {
        id: accountId,
      },
      transaction: {
        amount: amount,
        description: description,
        transactionDate: transactionDate
      }
    }
  }
  return model;
}
