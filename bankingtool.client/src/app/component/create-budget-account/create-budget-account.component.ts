import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BudgetService } from '../../core/service/budget.service';
import { CreateCreateBudgetAccountRequestObject } from '../../dto/request/budget/create-budget-account-request';
import { ResponseDto } from '../../dto/response-dto';

@Component({
  selector: 'app-create-budget-account',
  templateUrl: './create-budget-account.component.html',
  styleUrls: ['./create-budget-account.component.css']
})
export class CreateBudgetAccountComponent {
  budgetAccountForm: FormGroup;
  messageList: string[] = [];
  constructor(private fb: FormBuilder, private budgetService: BudgetService) {
    this.budgetAccountForm = this.fb.group({
      budgetName: ['', [Validators.required]],
      amount: ['', [Validators.required]],
    });
  }

  createBudgetAccount() {
    if (!this.budgetAccountForm.invalid) {
      this.messageList = [];
      let isValid: boolean = true;
      let values = this.budgetAccountForm.value;
      if (isNullOrEmpty(values.budgetName)) isValid = false;
      if (isNullOrEmpty(values.amount)) isValid = false;
      let model = CreateCreateBudgetAccountRequestObject(values.budgetName, values.amount);
      this.budgetService.getCreateAccountInitialLoad(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.messageList.push(response.message);
        } else {
          if (Array.isArray(response?.errors) && response?.errors)
            this.messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage), ...[response.message]]
        }
      })
    }
  }
}
