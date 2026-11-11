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
    return this.httpService.get(`CreditCardApplication/GetApplicationStatus?applicationId=${applicationId}`);
  }

  getCustomerApplications(accountId: number): Observable<any> {
    return this.httpService.get(`CreditCardApplication/GetApplicationHistory?accountId=${accountId}`);
  }

  getPendingApplications(): Observable<any> {
    return this.httpService.get('CreditCardApplication/GetPendingApplications');
  }

  approveCreditCard(applicationId: number, approvedLimit: number, approvalRemarks: string = ''): Observable<any> {
    return this.httpService.post(`CreditCardApplication/ApproveApplication?applicationId=${applicationId}&approvedLimit=${approvedLimit}&approvalRemarks=${approvalRemarks}`, {});
  }

  rejectCreditCard(applicationId: number, rejectionRemarks: string = ''): Observable<any> {
    return this.httpService.post(`CreditCardApplication/RejectApplication?applicationId=${applicationId}&rejectionRemarks=${rejectionRemarks}`, {});
  }

  // Credit Card Management Methods
  getCardDetails(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetCardDetails?creditCardId=${creditCardId}`);
  }

  getCustomerCreditCards(): Observable<any> {
    return this.httpService.get(`CreditCard/GetCustomerCreditCards`);
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
  recordTransaction(creditCardId: number, transactionType: string, amount: number, description: string, merchantName: string = '', referenceNumber: string = ''): Observable<any> {
    return this.httpService.post(
      `CreditCard/RecordTransaction?creditCardId=${creditCardId}&transactionType=${transactionType}&amount=${amount}&description=${description}&merchantName=${merchantName}&referenceNumber=${referenceNumber}`,
      {}
    );
  }

  processPayment(creditCardId: number, paymentAmount: number, description: string = 'Payment'): Observable<any> {
    return this.httpService.post(`CreditCard/ProcessPayment?creditCardId=${creditCardId}&paymentAmount=${paymentAmount}&description=${description}`, {});
  }

  getTransactionHistory(creditCardId: number, pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.httpService.get(`CreditCard/GetTransactionHistory?creditCardId=${creditCardId}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getTransactionsByDateRange(creditCardId: number, fromDate: Date, toDate: Date): Observable<any> {
    return this.httpService.get(`CreditCard/GetTransactionsByDateRange?creditCardId=${creditCardId}&fromDate=${fromDate}&toDate=${toDate}`);
  }

  getRecentTransactions(creditCardId: number, count: number = 10): Observable<any> {
    return this.httpService.get(`CreditCard/GetRecentTransactions?creditCardId=${creditCardId}&count=${count}`);
  }

  reverseTransaction(transactionId: number, reason: string = ''): Observable<any> {
    return this.httpService.post(`CreditCard/ReverseTransaction?transactionId=${transactionId}&reason=${reason}`, {});
  }

  // Statement Methods
  getCurrentStatement(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetCurrentStatement?creditCardId=${creditCardId}`);
  }

  getStatementHistory(creditCardId: number, pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.httpService.get(`CreditCard/GetStatementHistory?creditCardId=${creditCardId}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getOverdueStatements(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetOverdueStatements?creditCardId=${creditCardId}`);
  }

  // Dashboard Methods
  getDashboardData(accountId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetDashboardData?accountId=${accountId}`);
  }

  getCardBalance(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetCardBalance?creditCardId=${creditCardId}`);
  }

  getCardStatus(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetCardStatus?creditCardId=${creditCardId}`);
  }

  // Fee Methods
  getCardFees(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetCardFees?creditCardId=${creditCardId}`);
  }

  getPendingCharges(creditCardId: number): Observable<any> {
    return this.httpService.get(`CreditCard/GetPendingCharges?creditCardId=${creditCardId}`);
  }

  // Download Methods
  downloadStatementPDF(statementId: number): Observable<Blob> {
    return this.httpService.get(`CreditCard/DownloadStatementPDF?statementId=${statementId}`/* , { responseType: 'blob' } */);
  }
}
