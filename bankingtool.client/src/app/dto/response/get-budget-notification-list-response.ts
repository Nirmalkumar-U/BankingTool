export interface GetBudgetNotificationListResponse {
  budgetNotificationList: GetBudgetNotificationList[];
}

export interface GetBudgetNotificationList {
  budgetNotificationId: number;
  transactionType: string;
  transactionCatagory: string;
  description: string;
  amount: number;
  transactionDate: Date;
  notificationStatus: string;
  isSelected: boolean;
}
