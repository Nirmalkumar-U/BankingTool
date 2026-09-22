import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from '../../core/notificationService/notification-service';
import { MutualFundsService } from '../../core/service/mutual-funds.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { MutualFundsDto, UserInvestmentDto, InsertUserInvestmentRequest, MutualFundsInitialLoadResponse } from '../../dto/mutual-funds-dto';
import { InvestModalComponent } from './invest-modal/invest-modal.component';

// Interface for monthly investments within a scheme
interface MonthlyInvestmentDetail {
  monthYear: string;
  month: number;
  year: number;
  totalInvestmentAmount: number;
  totalUnits: number;
  investments: UserInvestmentDto[];
}

// Interface for scheme-wise grouped investments
interface SchemeInvestmentGroup {
  schemeName: string;
  schemeCode: string;
  totalInvestmentAmount: number;
  totalUnits: number;
  monthlyBreakdown: MonthlyInvestmentDetail[];
  expanded: boolean;
}

@Component({
  selector: 'app-investment',
  templateUrl: './investment.component.html',
  styleUrls: ['./investment.component.css']
})
export class InvestmentComponent implements OnInit {
  @ViewChild('investModal') investModal!: InvestModalComponent;

  // Math object for template use
  Math = Math;

  // Original data
  mutualFundsDbList: MutualFundsDto[] = [];
  userInvestmentList: UserInvestmentDto[] = [];

  // Pagination for Mutual Funds
  paginatedMutualFunds: MutualFundsDto[] = [];
  currentPageMutualFunds: number = 1;
  pageSize: number = 10;
  totalPagesMutualFunds: number = 0;

  // Pagination for User Investments - Now with scheme grouping
  allSchemeInvestments: SchemeInvestmentGroup[] = [];
  paginatedSchemeInvestments: SchemeInvestmentGroup[] = [];
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
    const initialData: ResponseDto<MutualFundsInitialLoadResponse> = this.activatedRoute.snapshot.data['DataResolver'];

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

    this.mutualFundsDbList = initialData.response.mutualFundsDbList || [];
    this.userInvestmentList = initialData.response.userInvestmentList || [];

    // Group investments by scheme with monthly breakdown
    this.groupInvestmentsByScheme();
    this.paginateUserInvestments(1);
  }

  // === GROUP INVESTMENTS BY SCHEME WITH MONTHLY BREAKDOWN ===
  groupInvestmentsByScheme() {
    const schemeMap = new Map<string, SchemeInvestmentGroup>();

    // Group by scheme
    this.userInvestmentList.forEach(investment => {
      const schemeKey = investment.schemeCode || investment.schemeName || 'Unknown';

      if (!schemeMap.has(schemeKey)) {
        schemeMap.set(schemeKey, {
          schemeName: investment.schemeName || 'Unknown',
          schemeCode: investment.schemeCode || '',
          totalInvestmentAmount: 0,
          totalUnits: 0,
          monthlyBreakdown: [],
          expanded: false
        });
      }

      const scheme = schemeMap.get(schemeKey)!;
      scheme.totalInvestmentAmount += investment.installmentAmount;
      scheme.totalUnits += investment.unitsPurchased;
    });

    // Now group by month within each scheme
    schemeMap.forEach((scheme) => {
      const schemeInvestments = this.userInvestmentList.filter(
        inv => (inv.schemeCode || inv.schemeName) === (scheme.schemeCode || scheme.schemeName)
      );

      const monthMap = new Map<string, MonthlyInvestmentDetail>();

      schemeInvestments.forEach(investment => {
        const date = new Date(investment.purchaseDate);
        const month = date.getMonth();
        const year = date.getFullYear();
        const monthYear = `${this.getMonthName(month)} ${year}`;
        const key = `${year}-${month}`;

        if (!monthMap.has(key)) {
          monthMap.set(key, {
            monthYear,
            month,
            year,
            totalInvestmentAmount: 0,
            totalUnits: 0,
            investments: []
          });
        }

        const monthDetail = monthMap.get(key)!;
        monthDetail.investments.push(investment);
        monthDetail.totalInvestmentAmount += investment.installmentAmount;
        monthDetail.totalUnits += investment.unitsPurchased;
      });

      // Convert to array and sort by year-month descending
      scheme.monthlyBreakdown = Array.from(monthMap.values()).sort((a, b) => {
        if (a.year !== b.year) {
          return b.year - a.year;
        }
        return b.month - a.month;
      });
    });

    // Convert to array and sort by scheme name
    this.allSchemeInvestments = Array.from(schemeMap.values()).sort((a, b) =>
      (a.schemeName || '').localeCompare(b.schemeName || '')
    );
  }

  getMonthName(month: number): string {
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June',
      'July', 'August', 'September', 'October', 'November', 'December'];
    return monthNames[month];
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

    this.totalPagesInvestments = Math.ceil(this.allSchemeInvestments.length / this.pageSizeInvestments);

    if (pageNumber > this.totalPagesInvestments && this.totalPagesInvestments > 0) return;

    this.currentPageInvestments = pageNumber;
    const startIndex = (pageNumber - 1) * this.pageSizeInvestments;
    const endIndex = startIndex + this.pageSizeInvestments;
    this.paginatedSchemeInvestments = this.allSchemeInvestments.slice(startIndex, endIndex);
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

  // === TOGGLE SCHEME EXPAND/COLLAPSE ===
  toggleSchemeExpand(schemeGroup: SchemeInvestmentGroup) {
    schemeGroup.expanded = !schemeGroup.expanded;
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

  openInvestModal() {
    if (this.investModal) {
      this.investModal.openModal();
    }
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
          this.groupInvestmentsByScheme();
          this.paginateUserInvestments(1);
        }
      }
    );
  }
}
