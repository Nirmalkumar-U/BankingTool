import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreditCardService } from '../../core/service/credit-card.service';
import { LoaderService } from '../../core/service/loader.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';

@Component({
  selector: 'app-credit-card-statement',
  templateUrl: './credit-card-statement.component.html',
  styleUrls: ['./credit-card-statement.component.css']
})
export class CreditCardStatementComponent implements OnInit {
  creditCardId: number = 0;
  currentStatement: any;
  statementHistory: any[] = [];
  isLoading: boolean = false;
  selectedStatement: any;

  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 0;

  showStatementDetails: boolean = false;

  constructor(
    private creditCardService: CreditCardService,
    private activatedRoute: ActivatedRoute,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.creditCardId = params['cardId'] || 0;
      if (this.creditCardId > 0) {
        this.loadCurrentStatement();
        this.loadStatementHistory(1);
      } else {
        this.notificationService.show('Invalid card ID', 'error');
        this.router.navigate(['/credit-card-dashboard']);
      }
    });
  }

  loadCurrentStatement(): void {
    this.loaderService.show();
    this.creditCardService.getCurrentStatement(this.creditCardId).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && response.response) {
          this.currentStatement = response.response;
        } else {
          this.notificationService.show('No current statement available', 'info');
        }
      },
      (error) => {
        this.loaderService.hide();
        console.error('Error loading current statement:', error);
        this.notificationService.show('Failed to load current statement', 'error');
      }
    );
  }

  loadStatementHistory(pageNumber: number = 1): void {
    this.loaderService.show();
    this.currentPage = pageNumber;

    this.creditCardService.getStatementHistory(this.creditCardId, pageNumber, this.pageSize).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.statementHistory = response.response;
          this.totalPages = Math.ceil((response.response?.length || 0) / this.pageSize);
        } else {
          this.statementHistory = [];
          this.notificationService.show('No statement history found', 'info');
        }
      },
      (error) => {
        this.loaderService.hide();
        console.error('Error loading statement history:', error);
        this.notificationService.show('Failed to load statement history', 'error');
      }
    );
  }

  viewStatementDetails(statement: any): void {
    this.selectedStatement = statement;
    this.showStatementDetails = true;
  }

  closeStatementDetails(): void {
    this.showStatementDetails = false;
    this.selectedStatement = null;
  }

  downloadStatement(statementId: number): void {
    this.loaderService.show();
    this.creditCardService.downloadStatementPDF(statementId).subscribe(
      (blob: Blob) => {
        this.loaderService.hide();
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = `statement_${statementId}.pdf`;
        link.click();
        window.URL.revokeObjectURL(url);
        this.notificationService.show('Statement downloaded successfully', 'success');
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to download statement', 'error');
        console.error('Error:', error);
      }
    );
  }

  printStatement(statement: any): void {
    const printWindow = window.open('', '', 'width=900,height=600');
    if (printWindow) {
      const html = this.getStatementHTML(statement);
      printWindow.document.write(html);
      printWindow.document.close();
      printWindow.print();
    }
  }

  getStatementHTML(statement: any): string {
    return `
      <html>
        <head>
          <title>Credit Card Statement</title>
          <style>
            body { font-family: Arial, sans-serif; margin: 20px; }
            .header { text-align: center; margin-bottom: 30px; }
            .section { margin: 20px 0; }
            .label { font-weight: bold; }
            table { width: 100%; border-collapse: collapse; margin: 20px 0; }
            th, td { border: 1px solid #ddd; padding: 10px; text-align: left; }
            th { background-color: #f2f2f2; }
            .total { font-weight: bold; background-color: #f9f9f9; }
          </style>
        </head>
        <body>
          <div class="header">
            <h2>Credit Card Statement</h2>
            <p>Statement Period: ${statement.statementStartDate ? new Date(statement.statementStartDate).toLocaleDateString() : ''} - ${statement.statementEndDate ? new Date(statement.statementEndDate).toLocaleDateString() : ''}</p>
          </div>

          <div class="section">
            <p><span class="label">Statement ID:</span> ${statement.creditCardStatementId}</p>
            <p><span class="label">Card Number:</span> ${statement.maskedCardNumber}</p>
            <p><span class="label">Cardholder:</span> ${statement.cardHolderName}</p>
          </div>

          <div class="section">
            <h3>Statement Summary</h3>
            <p><span class="label">Previous Balance:</span> $${(statement.previousBalance || 0).toFixed(2)}</p>
            <p><span class="label">Total Payments:</span> -$${(statement.totalPayments || 0).toFixed(2)}</p>
            <p><span class="label">New Charges:</span> +$${(statement.newCharges || 0).toFixed(2)}</p>
            <p><span class="label">Fees & Interest:</span> +$${(statement.feesAndInterest || 0).toFixed(2)}</p>
            <p><strong><span class="label">Current Balance:</span> $${(statement.currentBalance || 0).toFixed(2)}</strong></p>
            <p><span class="label">Minimum Payment Due:</span> $${(statement.minimumPaymentDue || 0).toFixed(2)}</p>
            <p><span class="label">Payment Due Date:</span> ${statement.paymentDueDate ? new Date(statement.paymentDueDate).toLocaleDateString() : ''}</p>
          </div>

          <div class="section">
            <h3>Transactions</h3>
            <table>
              <thead>
                <tr>
                  <th>Date</th>
                  <th>Description</th>
                  <th>Type</th>
                  <th>Amount</th>
                </tr>
              </thead>
              <tbody>
                ${statement.statementDetails ? statement.statementDetails.map((detail: any) => `
                  <tr>
                    <td>${detail.transactionDate ? new Date(detail.transactionDate).toLocaleDateString() : ''}</td>
                    <td>${detail.description}</td>
                    <td>${detail.transactionType}</td>
                    <td>$${(detail.amount || 0).toFixed(2)}</td>
                  </tr>
                `).join('') : ''}
              </tbody>
            </table>
          </div>

          <div class="section">
            <p style="font-size: 0.9em; color: #666;">This is an electronically generated document. No signature is required.</p>
          </div>
        </body>
      </html>
    `;
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value);
  }

  goToPage(pageNumber: number): void {
    if (pageNumber > 0 && pageNumber <= this.totalPages) {
      this.loadStatementHistory(pageNumber);
    }
  }
}
