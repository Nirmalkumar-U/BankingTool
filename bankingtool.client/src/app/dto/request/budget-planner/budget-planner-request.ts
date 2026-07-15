export interface CreateBudgetPlannerRequest {
  request: {
    budgetPlanner: {
      planName: string;
      description: string;
      totalBudget: number;
      budgetItems: BudgetItemRequest[];
    };
  };
}

export interface UpdateBudgetPlannerRequest {
  request: {
    budgetPlanner: {
      budgetPlannerId: number;
      planName: string;
      description: string;
      totalBudget: number;
    };
  };
}

export interface BudgetItemRequest {
  budgetName: string;
  allocatedAmount: number;
}
