import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { BudgetService } from '../../core/service/budget.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { CreateAcceptBudgetTransactionRequestObject } from '../../dto/request/budget/accept-budget-transaction-request';
import { CreateRejectBudgetTransactionRequestObject } from '../../dto/request/budget/reject-budget-transaction-request';
import { ResponseDto } from '../../dto/response-dto';
import { GetBudgetNotificationListResponse } from '../../dto/response/get-budget-notification-list-response';
//import { Modal } from 'bootstrap';

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
  transferAmountForm: FormGroup;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private budgetService: BudgetService) {
    this.transferAmountForm = this.fb.group({
      budgetAccount: this.fb.array([])
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<GetBudgetNotificationListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountInfo = initialData.response;
    this.budgetAccountList = initialData.dropDownList.find(x => x.name == "BudgetAccount")!.dropDown
    this.addBudgetAccount();
  }
  selectedBudgetAccount($event: any, budgetNotificationId: number) {
    $event.preventDefault();
    this.budgetAccountId = $event.target.value;
    this.selectedBudgetNotificationId = budgetNotificationId;
  }
  transferBudgetAmount(budgetNotificationId: number) {
    if (this.selectedBudgetNotificationId == budgetNotificationId) {
      let budgetAccounts: any[] = this.transferAmountForm.get('budgetAccount')?.value;
      let validBudgetAccounts = budgetAccounts
        .filter(x => !isNullOrEmpty(x.budgetAccountId) && !isNullOrEmpty(x.budgetAmount) && x.budgetNotificationId == this.selectedBudgetNotificationId)
        .map((z: any) => ({ id: z.budgetAccountId, amount: z.budgetAmount }));
      let model = CreateAcceptBudgetTransactionRequestObject(validBudgetAccounts, budgetNotificationId);
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
  selectBudgetAccount(budgetNotificationId: number) {
    this.selectedBudgetNotificationId = budgetNotificationId;
  }
  get budgetAccount(): FormArray {
    return this.transferAmountForm.get('budgetAccount') as FormArray;
  }

  addBudgetAccount(): void {
    this.budgetAccount.push(
      this.fb.group({
        budgetNotificationId: [this.selectedBudgetNotificationId],
        budgetAccountId: ['', Validators.required],
        budgetAmount: ['', Validators.required]
      })
    );
  }

  removeBudgetAccount(index: number): void {
    this.budgetAccount.removeAt(index);
  }

}
