import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BudgetService } from '../../core/service/budget.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateCreateBudgetAccountRequestObject } from '../../dto/request/budget/create-budget-account-request';

@Component({
  selector: 'app-create-budget-account',
  templateUrl: './create-budget-account.component.html',
  styleUrls: ['./create-budget-account.component.css']
})
export class CreateBudgetAccountComponent {
  budgetAccountForm: FormGroup;
  isSubmitted: boolean = false;
  constructor(private fb: FormBuilder, private budgetService: BudgetService, private notificationService: NotificationService) {
    this.budgetAccountForm = this.fb.group({
      budgetName: ['', [Validators.required]],
      amount: ['', [Validators.required]],
    });
  }

  createBudgetAccount() {
    if (!this.budgetAccountForm.invalid) {
      let isValid: boolean = true;
      let values = this.budgetAccountForm.value;
      if (isNullOrEmpty(values.budgetName)) isValid = false;
      if (isNullOrEmpty(values.amount)) isValid = false;
      let model = CreateCreateBudgetAccountRequestObject(values.budgetName, values.amount);
      this.budgetService.createBudgetAccount(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show(response.message, 'success');
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Create Budget Account failed.");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      })
    }
  }
}
