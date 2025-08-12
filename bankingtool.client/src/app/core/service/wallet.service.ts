import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateWalletRequestObject } from '../../dto/request/wallet/create-wallet-request';
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
}
