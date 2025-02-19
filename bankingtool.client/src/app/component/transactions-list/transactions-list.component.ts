import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ClaimKey } from '../../constant/constant';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { LocalStorageService } from '../../core/local-storage.service';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { GetTransactionsListDto } from '../../dto/get-transactions-list-dto';
import { ResponseDto } from '../../dto/response-dto';
import { TransactionsListAccountInfoDto } from '../../dto/transactions-list-account-info-dto';
import { TransactionsListCardInfoDto } from '../../dto/transactions-list-card-info-dto';
import { TransactionsListDto } from '../../dto/transactions-list-dto';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent {
  transactionForm: FormGroup;
  bankList: DropDownDto[] = [];
  accountTypeList: DropDownDto[] = [];
  customerId: number = 0;
  accountInfo: TransactionsListAccountInfoDto | null = null;
  cardInfo: TransactionsListCardInfoDto[] = [];
  transactionsList: TransactionsListDto[] = [];

  get accountTypeId(): number {
    return this.transactionForm.controls["accountTypeId"].value;
  }
  get bankId(): number {
    return this.transactionForm.controls["bankId"].value;
  }
  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private bankAccountService: BankAccountService,
    private localStorageService: LocalStorageService) {
    this.transactionForm = this.fb.group({
      bankId: ['', [Validators.required]],
      accountTypeId: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<DropDownDto[]> = this.activatedRoute.snapshot.data['DataResolver'];
    this.bankList = initialData.result;

    this.customerId = Number(this.localStorageService.getItem(ClaimKey.customerId));

    this.transactionForm.get('bankId')?.valueChanges.subscribe(bankId => {
      if (!isNullOrEmpty(bankId) && !isNullOrEmpty(this.customerId)) {
        this.bankAccountService.getAccountTypeDropDownListByCustomerIdAndBankId(this.customerId, bankId).subscribe((response: ResponseDto<DropDownDto[]>) => {
          this.accountTypeList = response.result;
        });
      }
    });
    this.transactionForm.get('accountTypeId')?.valueChanges.subscribe(accountTypeId => {
      if (!isNullOrEmpty(accountTypeId) && !isNullOrEmpty(this.bankId) && !isNullOrEmpty(this.customerId)) {
        this.bankAccountService.transactionsListForCustomer(this.bankId, this.accountTypeId, this.customerId).subscribe((response: ResponseDto<GetTransactionsListDto>) => {
          this.accountInfo = response.result.accountInfo;
          this.cardInfo = response.result.cardInfo;
          this.transactionsList = response.result.transactionsList;
        });
      }
    });
  }
}
