import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ClaimKey } from '../../constant/constant';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { LocalStorageService } from '../../core/local-storage.service';
import { BankAccountService } from '../../core/service/bank-account.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { GetAccountTypeDropDownListRequestObject } from '../../dto/request/bank-account/get-account-type-drop-down-list-request';
import { TransactionsListForCustomerRequestObject } from '../../dto/request/bank-account/transactions-list-for-customer-request';
import { ResponseDto } from '../../dto/response-dto';
import { GetTransactionsListResponse, GetTransactionsListResponseCardInfo, GetTransactionsListResponseTransactionList } from '../../dto/response/get-transactions-list-response';
import { TransactionsListAccountInfoDto } from '../../dto/transactions-list-account-info-dto';

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
  cardInfo: GetTransactionsListResponseCardInfo[] = [];
  transactionsList: GetTransactionsListResponseTransactionList[] = [];

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
    const initialData: ResponseDto<boolean> = this.activatedRoute.snapshot.data['DataResolver'];
    this.bankList = initialData.dropDownList.find(x => x.name == "Bank")!.dropDown;

    this.customerId = Number(this.localStorageService.getItem(ClaimKey.customerId));

    this.transactionForm.get('bankId')?.valueChanges.subscribe(bankId => {
      if (!isNullOrEmpty(bankId) && !isNullOrEmpty(this.customerId)) {
        let model: GetAccountTypeDropDownListRequestObject = {
          request: {
            bank: {
              id: bankId
            },
            customer: {
              id: this.customerId
            }
          }
        }
        this.bankAccountService.getAccountTypeDropDownListByCustomerIdAndBankId(model).subscribe((response: ResponseDto<boolean>) => {
          this.accountTypeList = response.dropDownList.find(x => x.name == "AccountType")!.dropDown;
        });
      }
      this.transactionForm.controls["accountTypeId"].setValue('');
    });
    this.transactionForm.get('accountTypeId')?.valueChanges.subscribe(accountTypeId => {
      if (!isNullOrEmpty(accountTypeId) && !isNullOrEmpty(this.bankId) && !isNullOrEmpty(this.customerId)) {
        let model: TransactionsListForCustomerRequestObject = {
          request: {
            account: {
              accountTypeId: this.accountTypeId
            },
            bank: {
              id: this.bankId
            },
            customer: {
              id: this.customerId
            }
          }
        }
        this.bankAccountService.transactionsListForCustomer(model).subscribe((response: ResponseDto<GetTransactionsListResponse>) => {
          this.accountInfo = response.response.accountInfo;
          this.cardInfo = response.response.cardInfo;
          this.transactionsList = response.response.transactionsList;
        });
      }
    });
  }
}
