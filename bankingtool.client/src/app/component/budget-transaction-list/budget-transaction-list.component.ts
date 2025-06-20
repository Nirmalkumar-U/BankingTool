import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from '../../core/notificationService/notification-service';
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

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private budgetService: BudgetService, private notificationService: NotificationService) { }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetTransactionListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Budget Transaction List page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.accountInfo = initialData.response;
  }
}
