export interface GetBudgetListResponse {
  budgetList: GetBudgetList[];
}

export interface GetBudgetList {
  budgetAccountId: number;
  budgetName: string;
  budgetNumber: string;
  balance: number;
  budgetStatus: string;
}
