import { CreditCardResponse } from "./credit-card-response";
import { TransactionResponse } from "./transaction-response";

export interface CreditCardDashboardInitialLoadResponse {
  creditCards: CreditCardResponse[];
  transactions: TransactionResponse[];
}
