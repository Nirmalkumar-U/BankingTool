import { TransactionsListAccountInfoDto } from "./transactions-list-account-info-dto";
import { TransactionsListCardInfoDto } from "./transactions-list-card-info-dto";
import { TransactionsListDto } from "./transactions-list-dto";

export interface GetTransactionsListDto {
  transactionsList: TransactionsListDto[];
  accountInfo: TransactionsListAccountInfoDto;
  cardInfo: TransactionsListCardInfoDto[];
}
