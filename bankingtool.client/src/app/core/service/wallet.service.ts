import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChangeWalletStatusRequestObject } from '../../dto/request/wallet/change-wallet-status-request';
import { CreateWalletRequestObject } from '../../dto/request/wallet/create-wallet-request';
import { GetWalletTransactionListRequestObject } from '../../dto/request/wallet/get-wallet-transaction-list-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class WalletService {
  constructor(private httpService: HttpService) { }

  createWallet(model: CreateWalletRequestObject): Observable<any> {
    return this.httpService.post('Wallet/CreateWallet', model);
  }
  getWalletList(): Observable<any> {
    return this.httpService.get('Wallet/GetWalletList');
  }
  getWalletDepositInitialLoad(): Observable<any> {
    return this.httpService.get('Wallet/GetWalletDepositInitialLoad');
  }
  getWalletTransferInitialLoad(): Observable<any> {
    return this.httpService.get('Wallet/GetWalletTransferInitialLoad');
  }
  getWalletTransactionList(model: GetWalletTransactionListRequestObject): Observable<any> {
    return this.httpService.post('Wallet/GetWalletTransactionList', model);
  }
  changeWalletStatus(model: ChangeWalletStatusRequestObject): Observable<any> {
    return this.httpService.post('Wallet/ChangeWalletStatus', model);
  }
  depositWalletAmount(model: ChangeWalletStatusRequestObject): Observable<any> {
    return this.httpService.post('Wallet/DepositWalletAmount', model);
  }
}
