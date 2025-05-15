export interface CreateBudgetAccountRequestObject {
  request: CreateBudgetAccountRequest;
}

export interface CreateBudgetAccountRequest {
  budgetAccount: CreateBudgetAccountRequestBudgetAccount;
}

export interface CreateBudgetAccountRequestBudgetAccount {
  budgetName: string;
  amount: number;
}

export const CreateCreateBudgetAccountRequestObject = (budgetName: string, amount: number): CreateBudgetAccountRequestObject => {
  let model: CreateBudgetAccountRequestObject = {
    request: {
      budgetAccount: {
        budgetName: budgetName,
        amount: amount
      }
    }
  }
  return model;
}
