export interface StatementResponseObject {
  response: StatementResponse;
}

export interface StatementResponseListObject {
  responses: StatementResponse[];
}

export interface StatementResponse {
  statement: StatementResponseStatement;
}

export interface StatementResponseStatement {
  id: number;
  billingCycleStartDate: string;
  billingCycleEndDate: string;
  statementDate: string;
  totalCharges: number;
  totalPayments: number;
  interest: number;
  fees: number;
  previousBalance: number;
  cardBalance: number;
  minimumDueAmount: number;
  dueDate: string;
  statementStatus: string;
  statementDetails: StatementResponseStatementDetail[];
}

export interface StatementResponseStatementDetail {
  id: number;
  transactionDate: string;
  description: string;
  amount: number;
  transactionType: string;
}
