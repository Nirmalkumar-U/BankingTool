import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { WalletService } from '../service/wallet.service';

@Injectable({
  providedIn: 'root'
})
export class WalletListResolver implements Resolve<boolean> {
  constructor(private walletService: WalletService) { }

  resolve(): Observable<boolean> {
    return this.walletService.getWalletList().pipe(
      catchError((error) => { return of(error) }));
  }
}
