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
  cardFees: any[] = [];
  isLoading: boolean = false;
  selectedStatement: any;
  statementDetails: any[] = [];
  cardDetails: any;
  showFees: boolean = false;
  feeFilter: string = '';
  feeTypes: string[] = ['Annual', 'LatePay', 'OverLimit', 'TransactionFee', 'InterestCharge', 'CashAdvance'];
  hasGeneratedStatement: boolean = false;
  billingCycleEndDate: string = '';
  statementStatusFilter: string = '';
  statementStatuses: string[] = ['Generated', 'Finalized', 'PartiallyPaid', 'Paid', 'Overdue'];

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
    this.activatedRoute.paramMap.subscribe(paramMap => {
      const pathCardId = Number(paramMap.get('cardId') || 0);

      this.activatedRoute.queryParamMap.subscribe(queryParamMap => {
        const queryCardId = Number(queryParamMap.get('cardId') || 0);
        this.creditCardId = pathCardId > 0 ? pathCardId : queryCardId;

        if (this.creditCardId > 0) {
          this.loadCardDetails();
          this.loadCurrentStatement();
          this.loadStatementHistory(1);
          this.loadCardFees();
          const nextCycleEnd = new Date();
          nextCycleEnd.setMonth(nextCycleEnd.getMonth() + 1);
          this.billingCycleEndDate = nextCycleEnd.toISOString().substring(0, 10);
          this.hasGeneratedStatement = false;
        } else {
          this.notificationService.show('Invalid card ID', 'error');
          this.router.navigate(['/creditCardDashboard']);
        }
      });
    });
  }

  loadCardDetails(): void {
    this.creditCardService.getCardDetails(this.creditCardId).subscribe(
      (response: ResponseDto<any>) => {
        if (response.status === true && response.response) {
          this.cardDetails = {
            ...response.response,
            maskedCardNumber: response.response.cardNumber?.maskedCardNumber || '****'
          };
        }
      },
      (error) => console.error('Error loading card details:', error)
    );
  }

  loadCardFees(): void {
    this.creditCardService.getCardFees(this.creditCardId, 1, 100).subscribe(
      (response: ResponseDto<any>) => {
        if (response.status === true && Array.isArray(response.response)) {
          this.cardFees = response.response;
        } else {
          this.cardFees = [];
        }
      },
      (error) => console.error('Error loading card fees:', error)
    );
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
    this.loaderService.show();
    this.creditCardService.getStatementWithDetails(statement.statementId).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && response.response) {
          this.selectedStatement = response.response;
          this.statementDetails = Array.isArray(response.response.statementDetails)
            ? response.response.statementDetails
            : [];
          this.showStatementDetails = true;
        } else {
          this.notificationService.show('Failed to load statement details', 'error');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to load statement details', 'error');
        console.error('Error loading statement details:', error);
      }
    );
  }

  generateStatement(): void {
    if (!this.billingCycleEndDate) {
      this.notificationService.show('Select a billing cycle end date', 'error');
      return;
    }

    this.loaderService.show();
    const endDate = new Date(`${this.billingCycleEndDate}T23:59:59`);
    this.creditCardService.generateStatement(this.creditCardId, endDate).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.hasGeneratedStatement = true;
          this.notificationService.show('Statement generated successfully', 'success');
          this.loadCurrentStatement();
          this.loadStatementHistory(1);
          this.loadCardFees();
        } else {
          this.notificationService.show('Failed to generate statement', 'error');
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to generate statement', 'error');
        console.error('Error generating statement:', error);
      }
    );
  }

  filterStatementsByStatus(): void {
    if (!this.statementStatusFilter) {
      this.loadStatementHistory(1);
      return;
    }

    this.loaderService.show();
    this.creditCardService.getStatementsByStatus(this.creditCardId, this.statementStatusFilter).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.statementHistory = response.response;
        } else {
          this.statementHistory = [];
        }
      },
      (error) => {
        this.loaderService.hide();
        console.error('Error filtering statements:', error);
      }
    );
  }

  filterFeesByType(): void {
    if (!this.feeFilter) {
      this.loadCardFees();
      return;
    }

    this.loaderService.show();
    this.creditCardService.getFeesByType(this.creditCardId, this.feeFilter).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true && Array.isArray(response.response)) {
          this.cardFees = response.response;
        } else {
          this.cardFees = [];
        }
      },
      (error) => {
        this.loaderService.hide();
        console.error('Error filtering fees:', error);
      }
    );
  }

  toggleFees(): void {
    this.showFees = !this.showFees;
  }

  markStatementPaid(statementId: number): void {
    this.loaderService.show();
    this.creditCardService.markStatementAsPaid(statementId).subscribe(
      (response: ResponseDto<any>) => {
        this.loaderService.hide();
        if (response.status === true) {
          this.notificationService.show('Statement marked as paid', 'success');
          this.loadCurrentStatement();
          this.loadStatementHistory(1);
        }
      },
      (error) => {
        this.loaderService.hide();
        this.notificationService.show('Failed to update statement', 'error');
      }
    );
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
            <p>Statement Period: ${statement.billingCycleStartDate ? new Date(statement.billingCycleStartDate).toLocaleDateString() : ''} - ${statement.billingCycleEndDate ? new Date(statement.billingCycleEndDate).toLocaleDateString() : ''}</p>
          </div>

          <div class="section">
            <p><span class="label">Statement ID:</span> ${statement.statementId}</p>
            <p><span class="label">Card Number:</span> ${this.cardDetails?.maskedCardNumber || '****'}</p>
            <p><span class="label">Cardholder:</span> ${this.cardDetails?.cardHolderName || '-'}</p>
          </div>

          <div class="section">
            <h3>Statement Summary</h3>
            <p><span class="label">Previous Balance:</span> $${(statement.previousBalance || 0).toFixed(2)}</p>
            <p><span class="label">Total Payments:</span> -$${(statement.totalPayments || 0).toFixed(2)}</p>
            <p><span class="label">New Charges:</span> +$${(statement.totalCharges || 0).toFixed(2)}</p>
            <p><span class="label">Fees & Interest:</span> +$${((statement.fees || 0) + (statement.interest || 0)).toFixed(2)}</p>
            <p><strong><span class="label">Current Balance:</span> $${(statement.cardBalance || 0).toFixed(2)}</strong></p>
            <p><span class="label">Minimum Payment Due:</span> $${(statement.minimumDueAmount || 0).toFixed(2)}</p>
            <p><span class="label">Payment Due Date:</span> ${statement.dueDate ? new Date(statement.dueDate).toLocaleDateString() : ''}</p>
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
