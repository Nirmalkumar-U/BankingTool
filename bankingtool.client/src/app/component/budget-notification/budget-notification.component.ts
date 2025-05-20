import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BudgetService } from '../../core/service/budget.service';
import { ResponseDto } from '../../dto/response-dto';
import { GetBudgetNotificationListResponse } from '../../dto/response/get-budget-notification-list-response';

@Component({
  selector: 'app-budget-notification',
  templateUrl: './budget-notification.component.html',
  styleUrls: ['./budget-notification.component.css']
})
export class BudgetNotificationComponent {
  accountInfo: GetBudgetNotificationListResponse | null = null;

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private budgetService: BudgetService) { }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetNotificationListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountInfo = initialData.response;
  }
}
