export interface GetBudgetPlannerListResponse {
  budgetPlanners: GetBudgetPlannerList[];
}

export interface GetBudgetPlannerList {
  BudgetPlannerId: number;
  PlanName: string;
  Description: string;
  TotalBudget: number;
  TotalItems: number;
  AllocatedAmount: number;
}

export interface GetBudgetPlannerDetailResponse {
  BudgetPlannerId: number;
  PlanName: string;
  Description: string;
  TotalBudget: number;
  BudgetItems: BudgetItemResponse[];
}

export interface BudgetItemResponse {
  BudgetItemId: number;
  BudgetName: string;
  AllocatedAmount: number;
}
