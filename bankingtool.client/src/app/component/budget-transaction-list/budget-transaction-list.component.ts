import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BudgetService } from '../../core/service/budget.service';
import { ResponseDto } from '../../dto/response-dto';
import { GetBudgetTransactionListResponse } from '../../dto/response/get-budget-transaction-list-response';

@Component({
  selector: 'app-budget-transaction-list',
  templateUrl: './budget-transaction-list.component.html',
  styleUrls: ['./budget-transaction-list.component.css']
})
export class BudgetTransactionListComponent {
  accountInfo: GetBudgetTransactionListResponse | null = null;

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private budgetService: BudgetService) { }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetTransactionListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountInfo = initialData.response;
  }
}
