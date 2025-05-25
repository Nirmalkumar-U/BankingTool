import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AcceptBudgetTransactionRequestObject } from '../../dto/request/budget/accept-budget-transaction-request';
import { CreateBudgetAccountRequestObject } from '../../dto/request/budget/create-budget-account-request';
import { GetBudgetTransactionListRequestObject } from '../../dto/request/budget/get-budget-transaction-list-request';
import { RejectBudgetTransactionRequestObject } from '../../dto/request/budget/reject-budget-transaction-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  constructor(private httpService: HttpService) { }

  getCreateAccountInitialLoad(model: CreateBudgetAccountRequestObject): Observable<any> {
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
}
