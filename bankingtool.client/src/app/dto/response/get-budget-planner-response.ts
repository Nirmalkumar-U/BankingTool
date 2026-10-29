export interface GetBudgetPlannerListResponse {
  budgetPlanners: GetBudgetPlannerList[];
}

export interface GetBudgetPlannerList {
  budgetPlannerId: number;
  planName: string;
  description: string;
  totalBudget: number;
  totalItems: number;
  allocatedAmount: number;
}

export interface GetBudgetPlannerDetailResponse {
  budgetPlannerId: number;
  planName: string;
  description: string;
  totalBudget: number;
  budgetItems: BudgetItemResponse[];
}

export interface BudgetItemResponse {
  budgetItemId: number;
  budgetName: string;
  allocatedAmount: number;
}
