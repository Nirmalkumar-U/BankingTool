import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { LocalStorageService } from '../local-storage.service';
import { BankAccountService } from '../service/bank-account.service';

@Injectable({
  providedIn: 'root'
})
export class SelfTransferResolver implements Resolve<boolean> {
  constructor(private bankAccountService: BankAccountService, private localStorageService: LocalStorageService) { }
  resolve(): Observable<any> {
    return this.bankAccountService.getSelfTransferInitialLoad().pipe(
      catchError((error) => { return of(error) }));
  }
}
