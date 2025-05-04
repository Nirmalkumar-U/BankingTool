import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateAccountRequestObject } from '../../dto/request/bank-account/create-account-request';
import { DepositAmountRequestObject } from '../../dto/request/bank-account/deposit-amount-request';
import { GetAccountBalanceRequestObject } from '../../dto/request/bank-account/get-account-balance-request';
import { GetAccountTypeDropDownListRequestObject } from '../../dto/request/bank-account/get-account-type-drop-down-list-request';
import { GetBankDetailWithoutCustomerAndAccountTypeRequestObject } from '../../dto/request/bank-account/get-bank-detail-without-customer-and-account-type-request';
import { GetToAccountListExcludedByAccountIdRequestObject } from '../../dto/request/bank-account/get-to-account-list-excluded-by-account-id-request';
import { IsCustomerHasCreditCardInThatBankRequestObject } from '../../dto/request/bank-account/is-customer-has-credit-card-in-that-bank-request';
import { TransactionsListForCustomerRequestObject } from '../../dto/request/bank-account/transactions-list-for-customer-request';
import { TransferAmountRequestObject } from '../../dto/request/bank-account/transfer-amount-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class BankAccountService {
  constructor(private httpService: HttpService) { }

  getCreateAccountInitialLoad(): Observable<any> {
    return this.httpService.get('BankAccount/GetCreateAccountInitialLoad');
  }
  createAccount(model: CreateAccountRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/CreateAccount', model);
  }
  getBankDetailsDropDownWithoutCustomerAndAccountType(model: GetBankDetailWithoutCustomerAndAccountTypeRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/GetBankDetailsWithoutCustomerAndAccountType', model);
  }
  isCustomerHasCreditCardInThatBank(model: IsCustomerHasCreditCardInThatBankRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/IsCustomerHasCreditCardInThatBank', model);
  }
  transactionsListForCustomer(model: TransactionsListForCustomerRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/TransactionsListForCustomer', model);
  }
  bankDropDownList(): Observable<any> {
    return this.httpService.get('BankAccount/BankDropDownList');
  }
  getAccountTypeDropDownListByCustomerIdAndBankId(model: GetAccountTypeDropDownListRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/GetAccountTypeDropDownListByCustomerIdAndBankId', model);
  }
  getTransferAmountInitialLoad(): Observable<any> {
    return this.httpService.get('BankAccount/GetTransferAmountInitialLoad');
  }
  getAccountBalance(model: GetAccountBalanceRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/GetAccountBalance', model);
  }
  transferAmount(model: TransferAmountRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/transferAmount', model);
  }
  getSelfTransferInitialLoad(): Observable<any> {
    return this.httpService.get('BankAccount/GetSelfTransferInitialLoad');
  }
  getToAccountListExcludedByAccountId(model: GetToAccountListExcludedByAccountIdRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/GetToAccountListExcludedByAccountId', model);
  }
  depositAmount(model: DepositAmountRequestObject): Observable<any> {
    return this.httpService.post('BankAccount/DepositAmount', model);
  }
}


//transactionsListForCustomer(bankId: number, accountTypeId: number, customerId: number): Observable < any > {
//  return this.httpService.get('BankAccount/TransactionsListForCustomer?customerId=' + customerId.toString() + "&accountTypeId=" + accountTypeId.toString() + "&bankId=" + bankId.toString());
//}
