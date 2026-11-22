import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../core/notificationService/notification-service';
import { CreditCardService } from '../../core/service/credit-card.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';

@Component({
  selector: 'app-credit-card-statement',
  templateUrl: './credit-card-statement.component.html',
  styleUrls: ['./credit-card-statement.component.css']
})
export class CreditCardStatementComponent implements OnInit {
  creditCardId: number = 0;
  statementHistory: any[] = [];
  selectedStatement: any;
  statementDetails: any[] = [];
  cardDetails: any;
  showStatementDetails: boolean = false;
  selectedStatementId: number = 0;

  constructor(
    private creditCardService: CreditCardService,
    private activatedRoute: ActivatedRoute,
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
          this.loadStatementHistory();
        } else {
          this.notificationService.show('Invalid card ID', 'error');
          this.router.navigate(['/creditCardDashboard']);
        }
      });
    });
  }

  loadStatementHistory(): void {
    this.creditCardService.getStatementHistory(this.creditCardId).subscribe(
      (response: ResponseDto<any>) => {
        if (response.status === true && Array.isArray(response.response)) {
          this.statementHistory = response.response;
          if (this.statementHistory.length > 0) {
            const selected = this.statementHistory[0];
            this.onStatementSelectionChange(selected.statementId);
          } else {
            this.statementHistory = [];
            this.selectedStatement = null;
            this.statementDetails = [];
          }
        } else {
          this.statementHistory = [];
        }
      },
      (error) => {
        this.statementHistory = [];
        console.error('Error loading statement history:', error);
      }
    );
  }

  onStatementSelectionChange(statementId: number): void {
    this.selectedStatementId = Number(statementId || 0);
    if (this.selectedStatementId > 0) {
      this.creditCardService.getStatementWithDetails(this.selectedStatementId).subscribe(
        (response: ResponseDto<any>) => {
          if (response.status === true && response.response) {
            this.selectedStatement = response.response;
            this.statementDetails = Array.isArray(response.response.statementDetails)
              ? response.response.statementDetails
              : [];
            this.showStatementDetails = false;
          }
        },
        (error) => {
          console.error('Error loading selected statement:', error);
        }
      );
    } else {
      this.selectedStatement = null;
      this.statementDetails = [];
    }
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value);
  }

  goToPage(pageNumber: number): void {
    if (pageNumber > 0) {
      this.loadStatementHistory();
    }
  }
}
