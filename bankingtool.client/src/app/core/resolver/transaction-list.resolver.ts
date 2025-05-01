import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { ClaimKey } from '../../constant/constant';
import { BankDropDownListRequestObject } from '../../dto/request/bank-account/bank-drop-down-list-request';
import { LocalStorageService } from '../local-storage.service';
import { BankAccountService } from '../service/bank-account.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionListResolver implements Resolve<boolean> {
  constructor(private bankAccountService: BankAccountService, private localStorageService: LocalStorageService) { }
  resolve(): Observable<any> {
    //let customerId: number = Number(this.localStorageService.getItem(ClaimKey.customerId));
    //let model: BankDropDownListRequestObject = {
    //  request: {
    //    customer: {
    //      id: customerId
    //    }
    //  }
    //}
    return this.bankAccountService.bankDropDownList().pipe(
      catchError((error) => { return of(error) }));
  }
}
