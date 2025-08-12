import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TableColumn } from '../../core/component/data-table/data-table.component';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetWalletTransactionListResponse } from '../../dto/response/get-wallet-transaction-list-response';

@Component({
  selector: 'app-wallet-transaction-list',
  templateUrl: './wallet-transaction-list.component.html',
  styleUrls: ['./wallet-transaction-list.component.css']
})
export class WalletTransactionListComponent {
  columns: TableColumn[] = [
    { field: 'transactionTag', header: 'Tag', sortable: true, width: '10%' },
    { field: 'transactionDate', header: 'Date', sortable: true, width: '15%', format: 'date', dateFormat: 'dd-MM-yyyy' },
    { field: 'description', header: 'Description', width: '25%' },
    { field: 'receiverAmount', header: 'Credit', sortable: true, width: '8%' },
    { field: 'senderAmount', header: 'Debit', sortable: true, width: '8%' },
    { field: 'stageBalance', header: 'Stage Balance', sortable: true, width: '12%' }
  ];

  accountInfo: GetWalletTransactionListResponse | null = null;

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private notificationService: NotificationService) { }
  ngOnInit() {
    const initialData: ResponseDto<GetWalletTransactionListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Wallet Transaction List page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.accountInfo = initialData.response;
  }
}
