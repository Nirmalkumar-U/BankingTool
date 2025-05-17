import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot, Resolve
} from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { CreateGetBudgetTransactionListRequestObject } from '../../dto/request/budget/get-budget-transaction-list-request';
import { BudgetService } from '../service/budget.service';

@Injectable({
  providedIn: 'root'
})
export class BudgetTransactionListResolver implements Resolve<boolean> {
  constructor(private budgetService: BudgetService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<boolean> {
    let budgetAccountId: number;
    budgetAccountId = +route.paramMap.get('id')!;
    let model = CreateGetBudgetTransactionListRequestObject(budgetAccountId);
    return this.budgetService.getBudgetTransactionList(model).pipe(
      catchError((error) => { return of(error) }));
  }
}
