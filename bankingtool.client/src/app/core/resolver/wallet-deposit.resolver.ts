import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { WalletService } from '../service/wallet.service';

@Injectable({
  providedIn: 'root'
})
export class WalletDepositResolver implements Resolve<boolean> {
  constructor(private bankAccountService: WalletService) { }
  resolve(): Observable<any> {
    return this.bankAccountService.getWalletDepositInitialLoad().pipe(
      catchError((error) => { return of(error) }));
  }
}
