export interface CreateWalletRequestObject {
  request: CreateWallettRequest;
}

export interface CreateWallettRequest {
  wallet: CreateWallettRequestWallet;
}

export interface CreateWallettRequestWallet {
  walletName: string;
  amount: number;
}

export const CreateCreateWallettRequestObject = (walletName: string, amount: number): CreateWalletRequestObject => {
  let model: CreateWalletRequestObject = {
    request: {
      wallet: {
        walletName: walletName,
        amount: amount
      }
    }
  }
  return model;
}
