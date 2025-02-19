import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { ClaimKey } from '../../constant/constant';
import { LocalStorageService } from '../local-storage.service';
import { BankAccountService } from '../service/bank-account.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionListResolver implements Resolve<boolean> {
  constructor(private bankAccountService: BankAccountService, private localStorageService: LocalStorageService) { }
  resolve(): Observable<any> {
    let customerId: number = Number(this.localStorageService.getItem(ClaimKey.customerId));
    return this.bankAccountService.bankDropDownList(customerId).pipe(
      catchError((error) => { return of(error) }));
  }
}
