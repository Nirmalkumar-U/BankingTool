import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {
  constructor(private httpService: HttpService) { }

  // Credit Card Application Methods
  applyCreditCard(accountId: number, cardType: string, desiredLimit: number): Observable<any> {
    const request = {
      accountId: accountId,
      cardType: cardType,
      desiredLimit: desiredLimit
    };
    return this.httpService.post('CreditCard/ApplyCreditCard', request);
  }

  getApplicationStatus(applicationId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetApplicationStatus?applicationId=${applicationId}`);
  }

  getCustomerApplications(accountId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetApplicationHistory?accountId=${accountId}`);
  }

  getCustomerCreditCards(): Observable<any> {
    return this.httpService.get('CreditCard/GetCustomerCreditCards');
  }

  approveCreditCard(applicationId: number, approvedLimit: number, approvalRemarks: string = ''): Observable<any> {
    return this.httpService.post(`CreditCard/ApproveApplication?applicationId=${applicationId}&approvedLimit=${approvedLimit}&approvalRemarks=${approvalRemarks}`, {});
  }

  rejectCreditCard(applicationId: number, rejectionRemarks: string = ''): Observable<any> {
    return this.httpService.post(`CreditCard/RejectApplication?applicationId=${applicationId}&rejectionRemarks=${rejectionRemarks}`, {});
  }

  // Credit Card Management Methods
  getCardDetails(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetCardDetails?creditCardId=${creditCardId}`);
  }

  updateCardLimit(creditCardId: number, newLimit: number): Observable<any> {
    return this.httpService.post(`CreditCard/UpdateCardLimit?creditCardId=${creditCardId}&newLimit=${newLimit}`, {});
  }

  closeCreditCard(creditCardId: number, closureReason: string = ''): Observable<any> {
    return this.httpService.post(`CreditCard/CloseCreditCard?creditCardId=${creditCardId}&closureReason=${closureReason}`, {});
  }

  blockCreditCard(creditCardId: number, blockReason: string = ''): Observable<any> {
    return this.httpService.post(`CreditCard/BlockCreditCard?creditCardId=${creditCardId}&blockReason=${blockReason}`, {});
  }

  unblockCreditCard(creditCardId: number): Observable<any> {
    return this.httpService.post(`CreditCard/UnblockCreditCard?creditCardId=${creditCardId}`, {});
  }

  getAvailableCredit(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetAvailableCredit?creditCardId=${creditCardId}`);
  }

  // Transaction Methods
  recordTransaction(request: {
    creditCardId: number;
    transactionType: string;
    amount: number;
    description: string;
    merchantName: string;
    budgetAccounts: any[];
    transactionDate: Date;
  }): Observable<any> {
    return this.httpService.post('CreditCard/RecordTransaction', request);
  }

  processPayment(request: {
    creditCardId: number;
    paymentAmount: number;
    description: string;
    transactionDate: Date;
    fees: number;
  }): Observable<any> {
    return this.httpService.post('CreditCard/ProcessPayment', request);
  }

  getTransactionHistory(creditCardId: number, pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.httpService.get(`CreditCard/GetTransactionHistory?creditCardId=${creditCardId}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getTransactionsByDateRange(creditCardId: number, fromDate: Date, toDate: Date): Observable<any> {
    return this.httpService.get(`CreditCard/GetTransactionsByDateRange?creditCardId=${creditCardId}&fromDate=${fromDate}&toDate=${toDate}`);
  }

  getCreditCardDashboardInitialLoad(): Observable<any> {
    return this.httpService.get('CreditCard/GetCreditCardDashboardInitialLoad');
  }

  // Statement Methods
  getStatementHistory(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetStatementHistory?creditCardId=${creditCardId}`);
  }

  getStatementWithDetails(statementId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetStatementWithDetails?statementId=${statementId}`);
  }
}
