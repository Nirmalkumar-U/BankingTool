import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TableColumn } from '../../core/component/data-table/data-table.component';
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
  columns: TableColumn[] = [
    { field: 'accountName', header: 'Budget AccountName', sortable: true, width: '13%' },
    { field: 'transactionTag', header: 'Tag', sortable: true, width: '8%' },
    { field: 'transactionDate', header: 'Date', sortable: true, width: '11%', format: 'date', dateFormat: 'dd-MM-yyyy' },
    { field: 'description', header: 'Description', width: '18%' },
    { field: 'receiverAmount', header: 'Credit', sortable: true, width: '8%' },
    { field: 'senderAmount', header: 'Debit', sortable: true, width: '8%' },
    { field: 'stageBalance', header: 'Stage Balance', sortable: true, width: '12%' }
  ];

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
