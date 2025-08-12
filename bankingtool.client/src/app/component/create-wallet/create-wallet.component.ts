import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { NotificationService } from '../../core/notificationService/notification-service';
import { WalletService } from '../../core/service/wallet.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateCreateWallettRequestObject } from '../../dto/request/wallet/create-wallet-request';

@Component({
  selector: 'app-create-wallet',
  templateUrl: './create-wallet.component.html',
  styleUrls: ['./create-wallet.component.css']
})
export class CreateWalletComponent {
  walletForm: FormGroup;
  isSubmitted: boolean = false;
  constructor(private fb: FormBuilder, private walletService: WalletService, private notificationService: NotificationService) {
    this.walletForm = this.fb.group({
      walletName: ['', [Validators.required]],
      amount: ['', [Validators.required]],
    });
  }

  createWallet() {
    let isValid: boolean = true;
    let values = this.walletForm.value;
    if (isNullOrEmpty(values.walletName)) isValid = false;
    if (isNullOrEmpty(values.amount)) isValid = false;
    if (isValid == true) {
      let model = CreateCreateWallettRequestObject(values.walletName, values.amount);
      this.walletService.createWallet(model).subscribe((response: ResponseDto<boolean>) => {
        if (response.status == true) {
          this.isSubmitted = true;
          this.notificationService.show(response.message, 'success');
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Create Wallet failed.");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      })
    }
  }
}
