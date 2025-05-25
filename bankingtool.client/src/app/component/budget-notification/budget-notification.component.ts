import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BudgetService } from '../../core/service/budget.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { CreateAcceptBudgetTransactionRequestObject } from '../../dto/request/budget/accept-budget-transaction-request';
import { CreateRejectBudgetTransactionRequestObject } from '../../dto/request/budget/reject-budget-transaction-request';
import { ResponseDto } from '../../dto/response-dto';
import { GetBudgetNotificationListResponse } from '../../dto/response/get-budget-notification-list-response';

@Component({
  selector: 'app-budget-notification',
  templateUrl: './budget-notification.component.html',
  styleUrls: ['./budget-notification.component.css']
})
export class BudgetNotificationComponent {
  accountInfo: GetBudgetNotificationListResponse | null = null;
  budgetAccountList: DropDownDto[] = [];
  budgetAccountId: number | null = null;
  selectedBudgetNotificationId: number | null = null;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private budgetService: BudgetService) { }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetNotificationListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountInfo = initialData.response;
    this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown
  }
  selectedBudgetAccount($event: any, budgetNotificationId: number) {
    $event.preventDefault();
    this.budgetAccountId = $event.target.value;
    this.selectedBudgetNotificationId = budgetNotificationId;
    console.log(this.budgetAccountId);
  }
  transferBudgetAmount(budgetNotificationId: number) {
    if (this.selectedBudgetNotificationId == budgetNotificationId) {
      let model = CreateAcceptBudgetTransactionRequestObject(this.budgetAccountId!, budgetNotificationId);
      this.budgetService.acceptBudgetTransaction(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.response) {
          this.budgetService.getBudgetNotificationList().subscribe((data: ResponseDto<GetBudgetNotificationListResponse>) => {
            this.accountInfo = data.response;
          });
        }
      });
    }
  }
  rejectBudgetAmount(budgetNotificationId: number) {
    let model = CreateRejectBudgetTransactionRequestObject(budgetNotificationId);
    this.budgetService.rejectBudgetTransaction(model).subscribe((response: ResponseDto<boolean>) => {
      if (response.response) {
        this.budgetService.getBudgetNotificationList().subscribe((data: ResponseDto<GetBudgetNotificationListResponse>) => {
          this.accountInfo = data.response;
        });
      }
    });
  }
}
