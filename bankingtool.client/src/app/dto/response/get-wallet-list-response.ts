export interface GetWalletListResponse {
  walletList: GetWalletList[];
}

export interface GetWalletList {
  walletId: number;
  walletName: string;
  walletNumber: string;
  balance: number;
  walletStatus: string;
}
