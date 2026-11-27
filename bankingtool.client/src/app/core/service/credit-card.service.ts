import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreditCardApplicationRequestObject } from '../../dto/request/credit-card/credit-card-application-request';
import { ProcessCreditCardPaymentRequestObject } from '../../dto/request/credit-card/process-credit-card-payment-request';
import { RecordCreditCardTransactionRequestObject } from '../../dto/request/credit-card/record-credit-card-transaction-request';
import { CreditCardDashboardInitialLoadResponse } from '../../dto/response/credit-card/credit-card-dashboard-initial-load-response';
import { CreditCardResponseListObject, CreditCardResponseObject,} from '../../dto/response/credit-card/credit-card-response';
import { StatementResponseListObject, StatementResponseObject } from '../../dto/response/credit-card/statement-response';
import { TransactionResponseListObject } from '../../dto/response/credit-card/transaction-response';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {
  constructor(private httpService: HttpService) { }

  applyCreditCard(request: CreditCardApplicationRequestObject): Observable<ResponseDto<boolean>> {
    return this.httpService.post('CreditCard/ApplyCreditCard', request);
  }

  getCustomerCreditCards(): Observable<ResponseDto<CreditCardResponseListObject>> {
    return this.httpService.get('CreditCard/GetCustomerCreditCards');
  }

  getCardDetails(creditCardId: number): Observable<ResponseDto<CreditCardResponseObject>> {
    return this.httpService.get(`CreditCard/GetCardDetails?creditCardId=${creditCardId}`);
  }

  updateCardLimit(creditCardId: number, newLimit: number): Observable<ResponseDto<boolean>> {
    return this.httpService.get(`CreditCard/UpdateCardLimit?creditCardId=${creditCardId}&newLimit=${newLimit}`);
  }

  closeCreditCard(creditCardId: number, closureReason: string = ''): Observable<ResponseDto<boolean>> {
    return this.httpService.get(`CreditCard/CloseCreditCard?creditCardId=${creditCardId}&closureReason=${encodeURIComponent(closureReason)}`);
  }

  blockCreditCard(creditCardId: number, blockReason: string = ''): Observable<ResponseDto<boolean>> {
    return this.httpService.get(`CreditCard/BlockCreditCard?creditCardId=${creditCardId}&blockReason=${encodeURIComponent(blockReason)}`);
  }

  unblockCreditCard(creditCardId: number): Observable<ResponseDto<boolean>> {
    return this.httpService.get(`CreditCard/UnblockCreditCard?creditCardId=${creditCardId}`);
  }

  recordTransaction(request: RecordCreditCardTransactionRequestObject): Observable<ResponseDto<boolean>> {
    return this.httpService.post('CreditCard/RecordTransaction', request);
  }

  processPayment(request: ProcessCreditCardPaymentRequestObject): Observable<ResponseDto<boolean>> {
    return this.httpService.post('CreditCard/ProcessPayment', request);
  }

  getTransactionHistory(creditCardId: number): Observable<ResponseDto<TransactionResponseListObject>> {
    return this.httpService.get(`CreditCard/GetTransactionHistory?creditCardId=${creditCardId}`);
  }

  getCreditCardDashboardInitialLoad(): Observable<ResponseDto<CreditCardDashboardInitialLoadResponse>> {
    return this.httpService.get('CreditCard/GetCreditCardDashboardInitialLoad');
  }

  getStatementHistory(creditCardId: number): Observable<ResponseDto<StatementResponseListObject>> {
    return this.httpService.get(`CreditCard/GetStatementHistory?creditCardId=${creditCardId}`);
  }

  getStatementWithDetails(statementId: number): Observable<ResponseDto<StatementResponseObject>> {
    return this.httpService.get(`CreditCard/GetStatementWithDetails?statementId=${statementId}`);
  }
}
