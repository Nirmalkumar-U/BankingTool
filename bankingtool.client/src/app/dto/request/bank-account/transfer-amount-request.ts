export interface TransferAmountRequestObject {
  request: TransferAmountRequest;
}

export interface TransferAmountRequest {
  account: TransferAmountRequestAccount;
  transaction: TransferAmountRequestTransaction;
}

export interface TransferAmountRequestAccount {
  fromAccountId: number;
  toAccountId: number;
}

export interface TransferAmountRequestTransaction {
  amount: number;
  description: string;
}

export const getTransferAmountRequestObject = (fromAccountId: number, toAccountId: number, amount: number, description: string): TransferAmountRequestObject => {
  let model: TransferAmountRequestObject = {
    request: {
      account: {
        toAccountId: toAccountId,
        fromAccountId: fromAccountId,
      },
      transaction: {
        amount: amount,
        description: description
      }
    }
  }
  return model;
}
