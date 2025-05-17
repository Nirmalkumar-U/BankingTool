import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { BudgetService } from '../../core/service/budget.service';
import { ResponseDto } from '../../dto/response-dto';
import { GetBudgetList, GetBudgetListResponse } from '../../dto/response/get-budget-list-response';

@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css']
})
export class BudgetListComponent {
  budgetList: GetBudgetList[] = [];
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private budgetService: BudgetService, private readonly router: Router) {
  }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.budgetList = initialData.response.budgetList;
  }
  getBudgetTransactionList(budgetAccountId: number) {
    console.log(budgetAccountId);

    this.router.navigate([AppRoute.budgetTransactionList(budgetAccountId)]);
  }
}
