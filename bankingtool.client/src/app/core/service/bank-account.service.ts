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
  getBankDetailsDropDownWithoutCustomerAndAccountType(customerId: number, accountTypeId: number): Observable<any> {
    return this.httpService.get('BankAccount/GetBankDetailsDropDownWithoutCustomerAndAccountType?customerId=' + customerId.toString() + "&accountTypeId=" + accountTypeId.toString());
  }
  isCustomerHasCreditCardInThatBank(customerId: number, bankId: number): Observable<any> {
    return this.httpService.get('BankAccount/IsCustomerHasCreditCardInThatBank?customerId=' + customerId.toString() + "&bankId=" + bankId.toString());
  }
  transactionsListForCustomer(bankId: number, accountTypeId: number, customerId: number): Observable<any> {
    return this.httpService.get('BankAccount/TransactionsListForCustomer?customerId=' + customerId.toString() + "&accountTypeId=" + accountTypeId.toString() + "&bankId=" + bankId.toString());
  }
  bankDropDownList(customerId: number): Observable<any> {
    return this.httpService.get('BankAccount/BankDropDownList?customerId=' + customerId.toString());
  }
  getAccountTypeDropDownListByCustomerIdAndBankId(customerId: number, bankId: number): Observable<any> {
    return this.httpService.get('BankAccount/GetAccountTypeDropDownListByCustomerIdAndBankId?customerId=' + customerId.toString() + "&bankId=" + bankId.toString());
  }
}
