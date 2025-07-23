import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { BudgetService } from '../service/budget.service';

@Injectable({
  providedIn: 'root'
})
export class BudgetTransferResolver implements Resolve<boolean> {
  constructor(private budgetService: BudgetService) { }

  resolve(): Observable<boolean> {
    return this.budgetService.getBudgetDropdownList().pipe(
      catchError((error) => { return of(error) }));
  }
}
