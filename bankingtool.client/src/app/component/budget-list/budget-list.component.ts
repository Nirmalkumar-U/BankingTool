import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BudgetService } from '../../core/service/budget.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateChangeBudgetAccountStatusRequestObject } from '../../dto/request/budget/change-budget-account-status-request';
import { GetBudgetList, GetBudgetListResponse } from '../../dto/response/get-budget-list-response';

@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css']
})
export class BudgetListComponent {
  budgetList: GetBudgetList[] = [];
  inActiveBudgetList: GetBudgetList[] = [];
  constructor(private readonly activatedRoute: ActivatedRoute, private budgetService: BudgetService,
    private readonly router: Router, private notificationService: NotificationService) {
  }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Budget List page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.budgetList = initialData.response.budgetList.filter(x => x.budgetStatus == 'Active');
    this.inActiveBudgetList = initialData.response.budgetList.filter(x => x.budgetStatus == 'InActive');
  }
  getBudgetTransactionList(budgetAccountId: number) {
    this.router.navigate([AppRoute.budgetTransactionList(budgetAccountId)]);
  }
  changeStatus(budgetAccountId: number) {
    if (budgetAccountId != null) {
      let model = CreateChangeBudgetAccountStatusRequestObject(budgetAccountId);
      this.budgetService.changeBudgetAccountStatus(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          const budget = this.budgetList.find(item => item.budgetAccountId === budgetAccountId);
          if (budget) {
            budget.budgetStatus = budget.budgetStatus === 'Active' ? 'InActive' : 'Active';
          }
          const inActiveBudget = this.inActiveBudgetList.find(item => item.budgetAccountId === budgetAccountId);
          if (inActiveBudget) {
            inActiveBudget.budgetStatus = inActiveBudget.budgetStatus === 'Active' ? 'InActive' : 'Active';
          }
          this.notificationService.show("Budget Account Status updated successfully.", 'success')
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Budget Account Status updated failed.");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      });
    }
  }
}
