import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { WalletService } from '../../core/service/wallet.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetWalletList, GetWalletListResponse } from '../../dto/response/get-wallet-list-response';

@Component({
  selector: 'app-wallet-list',
  templateUrl: './wallet-list.component.html',
  styleUrls: ['./wallet-list.component.css']
})
export class WalletListComponent {
  walletList: GetWalletList[] = [];
  inActiveWalletList: GetWalletList[] = [];
  constructor(private readonly activatedRoute: ActivatedRoute, private walletService: WalletService,
    private readonly router: Router, private notificationService: NotificationService) {
  }
  ngOnInit() {
    const initialData: ResponseDto<GetWalletListResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("Wallet List page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.walletList = initialData.response.walletList.filter(x => x.walletStatus == 'Active');
    this.inActiveWalletList = initialData.response.walletList.filter(x => x.walletStatus == 'InActive');
  }
  getWalletTransactionList(walletId: number) {
    this.router.navigate([AppRoute.walletTransactionList(walletId)]);
  }
  changeStatus(walletId: number) {
  //  if (budgetAccountId != null) {
  //    let model = CreateChangeBudgetAccountStatusRequestObject(budgetAccountId);
  //    this.budgetService.changeBudgetAccountStatus(model).subscribe((response: ResponseDto<boolean>) => {
  //      if (response.status == true) {
  //        const budget = this.budgetList.find(item => item.budgetAccountId === budgetAccountId);
  //        if (budget) {
  //          budget.budgetStatus = budget.budgetStatus === 'Active' ? 'InActive' : 'Active';
  //        }
  //        const inActiveBudget = this.inActiveBudgetList.find(item => item.budgetAccountId === budgetAccountId);
  //        if (inActiveBudget) {
  //          inActiveBudget.budgetStatus = inActiveBudget.budgetStatus === 'Active' ? 'InActive' : 'Active';
  //        }
  //        this.notificationService.show("Budget Account Status updated successfully.", 'success')
  //      } else {
  //        let messageList: string[] = [];
  //        if (Array.isArray(response?.errors) && response?.errors)
  //          messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
  //        messageList.push("Budget Account Status updated failed.");
  //        messageList.forEach((msg) => {
  //          this.notificationService.show(msg, 'error');
  //        });
  //      }
  //    });
  //  }
  }
}
