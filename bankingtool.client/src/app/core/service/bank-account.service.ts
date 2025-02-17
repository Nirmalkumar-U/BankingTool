import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateAccountDto } from '../../dto/create-account-dto';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class BankAccountService {
  constructor(private httpService: HttpService) { }

  getCreateAccountInitialLoad(): Observable<any> {
    return this.httpService.get('BankAccount/GetCreateAccountInitialLoad');
  }
  createAccount(model: CreateAccountDto): Observable<any> {
    return this.httpService.post('BankAccount/CreateAccount', model);
  }
  getBankDetailsByWithoutCustomerIdAndAccountTypeDropDown(customerId: number, accountTypeId: number): Observable<any> {
    return this.httpService.get('BankAccount/GetBankDetailsByWithoutCustomerIdAndAccountTypeDropDown?customerId=' + customerId.toString() + "&accountTypeId=" + accountTypeId.toString());
  }
  isCustomerHasCreditCardInThatBank(customerId: number, bankId: number): Observable<any> {
    return this.httpService.get('BankAccount/IsCustomerHasCreditCardInThatBank?customerId=' + customerId.toString() + "&bankId=" + bankId.toString());
  }
}
