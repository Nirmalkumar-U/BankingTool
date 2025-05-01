import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { ClaimKey } from '../../constant/constant';
import { GetTransferAmountInitialLoadRequestObject } from '../../dto/request/bank-account/get-transfer-amount-initial-load-request';
import { LocalStorageService } from '../local-storage.service';
import { BankAccountService } from '../service/bank-account.service';

@Injectable({
  providedIn: 'root'
})
export class TransferAmountResolver implements Resolve<boolean> {
  constructor(private bankAccountService: BankAccountService, private localStorageService: LocalStorageService) { }
  resolve(): Observable<any> {
    //let customerId: number = Number(this.localStorageService.getItem(ClaimKey.customerId));
    //let model: GetTransferAmountInitialLoadRequestObject = {
    //  request: {
    //    customer: {
    //      id: customerId
    //    }
    //  }
    //}
    return this.bankAccountService.getTransferAmountInitialLoad().pipe(
      catchError((error) => { return of(error) }));
  }
}
