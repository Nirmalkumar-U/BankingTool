import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { ResponseDto } from '../../dto/response-dto';
import { DashBoardAccountList, GetDashBoardInitialLoadResponse } from '../../dto/response/get-dash-board-initial-load-response';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  accountList: DashBoardAccountList[] = [];
  totalBalance: number = 0;
  totalAccounts: number = 0;
  totalSavingsAccounts: number = 0;
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private readonly router: Router) {
  }
  ngOnInit() {
    const initialData: ResponseDto<GetDashBoardInitialLoadResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.accountList = initialData.response.dashBoardAccountList;
    this.totalBalance = initialData.response.totalBalance;
    this.totalAccounts = this.accountList.length;
    this.totalSavingsAccounts = this.accountList.filter(account => account.accountType.toLowerCase() === 'saving account').length;
  }
  redirectTransferAmount() {
    this.router.navigate([AppRoute.transfer]);
  }
  redirectTransactionList(accountType: string, accountId: number) {
    if (accountType == 'Saving Account')
      this.router.navigate([AppRoute.transactions]);

    if (accountType == 'Budget Account')
      this.router.navigate([AppRoute.budgetTransactionList(accountId)]);
  }
}
