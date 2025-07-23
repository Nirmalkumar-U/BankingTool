import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AcceptBudgetTransactionRequestObject } from '../../dto/request/budget/accept-budget-transaction-request';
import { ChangeBudgetAccountStatusRequestObject } from '../../dto/request/budget/change-budget-account-status-request';
import { CreateBudgetAccountRequestObject } from '../../dto/request/budget/create-budget-account-request';
import { GetBudgetTransactionListRequestObject } from '../../dto/request/budget/get-budget-transaction-list-request';
import { GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject } from '../../dto/request/budget/get-to-budget-dropdown-list-and-balance-of-budget-account-request';
import { RejectBudgetTransactionRequestObject } from '../../dto/request/budget/reject-budget-transaction-request';
import { SaveBudgetTransactionRequestObject } from '../../dto/request/budget/save-budget-transaction-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  constructor(private httpService: HttpService) { }

  createBudgetAccount(model: CreateBudgetAccountRequestObject): Observable<any> {
    return this.httpService.post('Budget/CreateBudgetAccount', model);
  }
  getBudgetList(): Observable<any> {
    return this.httpService.get('Budget/GetBudgetList');
  }
  getBudgetTransactionList(model: GetBudgetTransactionListRequestObject): Observable<any> {
    return this.httpService.post('Budget/GetBudgetTransactionList', model);
  }
  getBudgetNotificationList(): Observable<any> {
    return this.httpService.get('Budget/GetBudgetNotificationList');
  }
  acceptBudgetTransaction(model: AcceptBudgetTransactionRequestObject): Observable<any> {
    return this.httpService.post('Budget/AcceptBudgetTransaction', model);
  }
  rejectBudgetTransaction(model: RejectBudgetTransactionRequestObject): Observable<any> {
    return this.httpService.post('Budget/RejectBudgetTransaction', model);
  }
  changeBudgetAccountStatus(model: ChangeBudgetAccountStatusRequestObject): Observable<any> {
    return this.httpService.post('Budget/ChangeBudgetAccountStatus', model);
  }
  getBudgetDropdownList(): Observable<any> {
    return this.httpService.get('Budget/GetBudgetDropdownList');
  }
  getToBudgetDropdownListAndBalanceOfBudgetAccount(model: GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject): Observable<any> {
    return this.httpService.post('Budget/GetToBudgetDropdownListAndBalanceOfBudgetAccount', model);
  }
  saveBudgetTransaction(model: SaveBudgetTransactionRequestObject): Observable<any> {
    return this.httpService.post('Budget/SaveBudgetTransaction', model);
  }
}
