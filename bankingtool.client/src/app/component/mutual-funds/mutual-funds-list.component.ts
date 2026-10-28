import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TableColumn } from '../../core/component/data-table/data-table.component';
import { NotificationService } from '../../core/notificationService/notification-service';
import { MutualFundsService } from '../../core/service/mutual-funds.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { MutualFundsDto, UserInvestmentDto, InsertUserInvestmentRequest, MutualFundsInitialLoadResponse } from '../../dto/mutual-funds-dto';


@Component({
  selector: 'app-mutual-funds-list',
  templateUrl: './mutual-funds-list.component.html',
  styleUrls: ['./mutual-funds-list.component.css']
})
export class MutualFundsListComponent implements OnInit {
  columns: TableColumn[] = [
    { field: 'schemeCode', header: 'Scheme Code', sortable: true, width: '13%' },
    { field: 'isinDivReinvestment', header: 'Scheme Name', sortable: true, width: '13%' },
    { field: 'schemeName', header: 'Category', sortable: true, width: '10%' },
    { field: 'mutualFundValue', header: 'Mutual Fund Value', sortable: true, width: '8%' },
    { field: 'mutualFundDate', header: 'Date', sortable: true, width: '11%', format: 'date', dateFormat: 'dd/MM/yyyy' }
  ];
  // Math object for template use
  Math = Math;

  // Original data
  mutualFundsDbList: MutualFundsDto[] = [];
  mutualFundsList: MutualFundsDto[] = [];
  userInvestmentList: UserInvestmentDto[] = [];

  // Pagination for Mutual Funds
  paginatedMutualFunds: MutualFundsDto[] = [];
  currentPageMutualFunds: number = 1;
  pageSize: number = 10;
  totalPagesMutualFunds: number = 0;

  // Pagination for User Investments
  paginatedUserInvestments: UserInvestmentDto[] = [];
  currentPageInvestments: number = 1;
  pageSizeInvestments: number = 5;
  totalPagesInvestments: number = 0;

  selectedSchemes: string[] = [];

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private mutualFundsService: MutualFundsService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.loadInitialData();
  }

  loadInitialData() {
    const initialData: ResponseDto<MutualFundsDto[]> = this.activatedRoute.snapshot.data['DataResolver'];

    if (initialData.status === false) {
      const messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors) {
        messageList.push(...initialData?.errors?.map(x => x.errorMessage || 'Unknown error'));
      }
      if (Array.isArray(initialData?.validationErrors) && initialData?.validationErrors) {
        messageList.push(...initialData?.validationErrors?.map(x => x.errorMessage || 'Validation error'));
      }
      messageList.push('Mutual Funds page load failed.');
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error');
      });
      return;
    }

    this.mutualFundsList = initialData.response;

    // Initialize pagination
    this.paginateMutualFunds(1);
    this.paginateUserInvestments(1);
  }

  // === MUTUAL FUNDS PAGINATION ===
  paginateMutualFunds(pageNumber: number) {
    if (pageNumber < 1) return;

    this.totalPagesMutualFunds = Math.ceil(this.mutualFundsList.length / this.pageSize);

    if (pageNumber > this.totalPagesMutualFunds && this.totalPagesMutualFunds > 0) return;

    this.currentPageMutualFunds = pageNumber;
    const startIndex = (pageNumber - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedMutualFunds = this.mutualFundsList.slice(startIndex, endIndex);
  }

  goToPageMutualFunds(pageNumber: number) {
    this.paginateMutualFunds(pageNumber);
  }

  getPagesMutualFunds(): number[] {
    const pages: number[] = [];
    for (let i = 1; i <= this.totalPagesMutualFunds; i++) {
      pages.push(i);
    }
    return pages;
  }

  // === USER INVESTMENTS PAGINATION ===
  paginateUserInvestments(pageNumber: number) {
    if (pageNumber < 1) return;

    this.totalPagesInvestments = Math.ceil(this.userInvestmentList.length / this.pageSizeInvestments);

    if (pageNumber > this.totalPagesInvestments && this.totalPagesInvestments > 0) return;

    this.currentPageInvestments = pageNumber;
    const startIndex = (pageNumber - 1) * this.pageSizeInvestments;
    const endIndex = startIndex + this.pageSizeInvestments;
    this.paginatedUserInvestments = this.userInvestmentList.slice(startIndex, endIndex);
  }

  goToPageInvestments(pageNumber: number) {
    this.paginateUserInvestments(pageNumber);
  }

  getPagesInvestments(): number[] {
    const pages: number[] = [];
    for (let i = 1; i <= this.totalPagesInvestments; i++) {
      pages.push(i);
    }
    return pages;
  }

  addScheme(scheme: MutualFundsDto) {
    if (!this.selectedSchemes.includes(scheme.schemeCode)) {
      this.selectedSchemes.push(scheme.schemeCode);
      this.notificationService.show(`${scheme.schemeName} added to selection`, 'success');
    } else {
      this.notificationService.show(`${scheme.schemeName} is already selected`, 'info');
    }
  }

  removeScheme(schemeCode: string) {
    this.selectedSchemes = this.selectedSchemes.filter(x => x !== schemeCode);
    this.notificationService.show('Scheme removed from selection', 'success');
  }

  saveSchemes() {
    if (this.selectedSchemes.length === 0) {
      this.notificationService.show('Please select at least one scheme', 'warning');
      return;
    }

    this.mutualFundsService.saveSchemes(this.selectedSchemes).subscribe(
      (response: ResponseDto<boolean>) => {
        if (response.status) {
          this.selectedSchemes = [];
          this.notificationService.show('Schemes saved successfully', 'success');
          // Refresh the list
          this.refreshData();
        } else {
          const messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors) {
            messageList.push(...response?.errors?.map(x => x.errorMessage || 'Unknown error'));
          }
          if (Array.isArray(response?.validationErrors) && response?.validationErrors) {
            messageList.push(...response?.validationErrors?.map(x => x.errorMessage || 'Validation error'));
          }
          messageList.push('Failed to save schemes');
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      },
      (error) => {
        this.notificationService.show('Error saving schemes: ' + error.message, 'error');
      }
    );
  }

  onInvestmentSubmitted(investment: InsertUserInvestmentRequest) {
    this.mutualFundsService.insertUserInvestment(investment).subscribe(
      (response: ResponseDto<boolean>) => {
        if (response.status) {
          this.notificationService.show('Investment added successfully', 'success');
          this.refreshData();
        } else {
          const messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors) {
            messageList.push(...response?.errors?.map(x => x.errorMessage || 'Unknown error'));
          }
          if (Array.isArray(response?.validationErrors) && response?.validationErrors) {
            messageList.push(...response?.validationErrors?.map(x => x.errorMessage || 'Validation error'));
          }
          messageList.push('Failed to add investment');
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      },
      (error) => {
        this.notificationService.show('Error adding investment: ' + error.message, 'error');
      }
    );
  }

  refreshData() {
    this.mutualFundsService.getAllUserInvestment().subscribe(
      (response: ResponseDto<UserInvestmentDto[]>) => {
        if (response.status) {
          this.userInvestmentList = response.response || [];
          this.paginateUserInvestments(1);
        }
      }
    );
  }

  getSchemeNameByCode(schemeCode: string): string {
    const scheme = this.mutualFundsList.find(x => x.schemeCode === schemeCode);
    return scheme ? scheme.schemeName : schemeCode;
  }
}
