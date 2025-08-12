import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot, Resolve
} from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { CreateGetWalletTransactionListRequestObject } from '../../dto/request/wallet/get-wallet-transaction-list-request';
import { WalletService } from '../service/wallet.service';

@Injectable({
  providedIn: 'root'
})
export class WalletTransactionListResolver implements Resolve<boolean> {
  constructor(private budgetService: WalletService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<boolean> {
    let walletId: number;
    walletId = +route.paramMap.get('id')!;
    let model = CreateGetWalletTransactionListRequestObject(walletId);
    return this.budgetService.getWalletTransactionList(model).pipe(
      catchError((error) => { return of(error) }));
  }
}
