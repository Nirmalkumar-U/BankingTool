import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { BankAccountService } from '../service/bank-account.service';

@Injectable({
  providedIn: 'root'
})
export class CashWithdrawResolver implements Resolve<boolean> {
  constructor(private bankAccountService: BankAccountService) { }
  resolve(): Observable<any> {
    return this.bankAccountService.getSelfTransferInitialLoad().pipe(
      catchError((error) => { return of(error) }));
  }
}
