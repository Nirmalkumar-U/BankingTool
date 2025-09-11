import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { WalletService } from '../../core/service/wallet.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateChangeWalletStatusRequestObject } from '../../dto/request/wallet/change-wallet-status-request';
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
    if (walletId != null) {
      let model = CreateChangeWalletStatusRequestObject(walletId);
      this.walletService.changeWalletStatus(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          const wallet = this.walletList.find(item => item.walletId === walletId);
          if (wallet) {
            wallet.walletStatus = wallet.walletStatus === 'Active' ? 'InActive' : 'Active';
          }
          const inActiveWallet = this.inActiveWalletList.find(item => item.walletId === walletId);
          if (inActiveWallet) {
            inActiveWallet.walletStatus = inActiveWallet.walletStatus === 'Active' ? 'InActive' : 'Active';
          }
          this.notificationService.show("Wallet Status updated successfully.", 'success')
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Wallet Status updated failed.");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      });
    }
  }
}
