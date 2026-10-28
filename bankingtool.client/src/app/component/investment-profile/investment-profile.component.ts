import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MutualFundsService } from '../../core/service/mutual-funds.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { UserInvestmentProfileDto } from '../../dto/user-investment-profile-dto';

interface MonthlyDetail {
  investedDate: Date | undefined;
  monthYear: string;
  profile: UserInvestmentProfileDto;
  navPrice: number;
  monthlyAllocationPercent: number;
  previousMonthValue?: number;
  monthOverMonthGrowth?: number;
  monthOverMonthGrowthPercent?: number;
  isExpanded: boolean;
}

interface SchemeGroup {
  schemeName: string;
  schemeCode: string;
  monthlyInvestments: MonthlyDetail[];
  totalInvested: number;
  totalCurrentValue: number;
  totalUnrealisedGain: number;
  totalRealisedGain: number;
  totalXirrPercent: number;
  isExpanded: boolean;
}

@Component({
  selector: 'app-investment-profile',
  templateUrl: './investment-profile.component.html',
  styleUrls: ['./investment-profile.component.css']
})
export class InvestmentProfileComponent implements OnInit {
  investmentProfiles: UserInvestmentProfileDto[] = [];
  schemeGroups: SchemeGroup[] = [];
  totalInvested: number = 0;
  totalCurrentValue: number = 0;
  totalUnrealisedGain: number = 0;
  totalRealisedGain: number = 0;
  overallAbsoluteReturn: number = 0;
  totalMonthlyInvestments: number = 0;

  constructor(
    private activatedRoute: ActivatedRoute,
    private mutualFundsService: MutualFundsService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.loadInvestmentProfiles();
  }

  loadInvestmentProfiles() {
    const initialData: ResponseDto<UserInvestmentProfileDto[]> = 
      this.activatedRoute.snapshot.data['DataResolver'];

    if (!initialData || initialData.status === false) {
      const messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors) {
        messageList.push(...initialData?.errors?.map(x => x.errorMessage || 'Unknown error'));
      }
      if (Array.isArray(initialData?.validationErrors) && initialData?.validationErrors) {
        messageList.push(...initialData?.validationErrors?.map(x => x.errorMessage || 'Validation error'));
      }
      messageList.push('Failed to load investment profiles.');
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error');
      });
      return;
    }

    this.investmentProfiles = initialData.response || [];
    this.groupInvestmentsByScheme();
    this.calculateTotals();
  }

  groupInvestmentsByScheme() {
    const groupMap = new Map<string, SchemeGroup>();

    // First pass: group by scheme
    this.investmentProfiles.forEach(profile => {
      const key = profile.schemeCode || profile.schemeName;

      if (!groupMap.has(key)) {
        groupMap.set(key, {
          schemeName: profile.schemeName,
          schemeCode: profile.schemeCode,
          monthlyInvestments: [],
          totalInvested: 0,
          totalCurrentValue: 0,
          totalUnrealisedGain: 0,
          totalRealisedGain: 0,
          totalXirrPercent: 0,
          isExpanded: false
        });
      }

      const scheme = groupMap.get(key)!;
      const monthlyDetail: MonthlyDetail = {
        investedDate: profile.investedDate,
        monthYear: this.getInvestedMonthYear(profile.investedDate),
        profile: profile,
        navPrice: profile.investmentCost > 0 ? profile.investmentCost / profile.totalUnitsPurchased : 0,
        monthlyAllocationPercent: 0,
        isExpanded: false
      };

      scheme.monthlyInvestments.push(monthlyDetail);
      scheme.totalInvested += profile.investmentCost;
      scheme.totalCurrentValue += profile.currentValue;
      scheme.totalUnrealisedGain += profile.unrealisedGain;
      scheme.totalRealisedGain += profile.realisedGain;
    });

    // Convert to array and sort
    this.schemeGroups = Array.from(groupMap.values())
      .sort((a, b) => a.schemeName.localeCompare(b.schemeName));

    // Second pass: calculate monthly metrics and comparisons
    this.schemeGroups.forEach(scheme => {
      // Sort monthly investments by date
      scheme.monthlyInvestments.sort((a, b) => {
        const dateA = new Date(a.investedDate || 0).getTime();
        const dateB = new Date(b.investedDate || 0).getTime();
        return dateA - dateB;
      });

      // Calculate monthly allocation percentages and comparisons
      scheme.monthlyInvestments.forEach((month, index) => {
        month.monthlyAllocationPercent = (month.profile.currentValue / scheme.totalCurrentValue) * 100;

        // Calculate month-over-month growth
        if (index > 0) {
          const prevMonth = scheme.monthlyInvestments[index - 1];
          month.previousMonthValue = prevMonth.profile.currentValue;
          month.monthOverMonthGrowth = month.profile.currentValue - prevMonth.profile.currentValue;
          month.monthOverMonthGrowthPercent = (month.monthOverMonthGrowth / prevMonth.profile.currentValue) * 100;
        }
      });

      // Calculate average XIRR
      if (scheme.monthlyInvestments.length > 0) {
        scheme.totalXirrPercent = scheme.monthlyInvestments.reduce((sum, inv) => sum + (inv.profile.xirrPercent || 0), 0) / scheme.monthlyInvestments.length;
      }
    });

    this.totalMonthlyInvestments = this.investmentProfiles.length;
  }

  calculateTotals() {
    this.totalInvested = 0;
    this.totalCurrentValue = 0;
    this.totalUnrealisedGain = 0;
    this.totalRealisedGain = 0;

    this.investmentProfiles.forEach(profile => {
      this.totalInvested += profile.investmentCost;
      this.totalCurrentValue += profile.currentValue;
      this.totalUnrealisedGain += profile.unrealisedGain;
      this.totalRealisedGain += profile.realisedGain;
    });

    if (this.totalInvested > 0) {
      this.overallAbsoluteReturn = ((this.totalCurrentValue - this.totalInvested) / this.totalInvested) * 100;
    }
  }

  toggleSchemeExpand(scheme: SchemeGroup) {
    scheme.isExpanded = !scheme.isExpanded;
  }

  toggleMonthExpand(month: MonthlyDetail) {
    month.isExpanded = !month.isExpanded;
  }

  getReturnClass(value: number): string {
    if (value > 0) {
      return 'positive';
    } else if (value < 0) {
      return 'negative';
    }
    return 'neutral';
  }

  getTrendIndicator(value?: number): string {
    if (value === undefined || value === null) return '';
    if (value > 0) return '📈';
    if (value < 0) return '📉';
    return '➡️';
  }

  formatNumber(value: number, decimals: number = 2): string {
    return value.toFixed(decimals);
  }

  getInvestedMonthYear(date: Date | undefined): string {
    if (!date) {
      return 'N/A';
    }
    const investmentDate = new Date(date);
    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
      'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const month = monthNames[investmentDate.getMonth()];
    const year = investmentDate.getFullYear();
    return `${month} ${year}`;
  }
}
