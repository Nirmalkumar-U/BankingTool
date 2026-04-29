export interface GetDashBoardInitialLoadResponse {
  dashBoardAccountList: DashBoardAccountList[];
  totalBalance: number;
  totalBudgetBalance: number;
}

export interface DashBoardAccountList {
  bankName: string;
  accountNumber: string;
  accountType: string;
  balance: number;
  accountId: number;
}
