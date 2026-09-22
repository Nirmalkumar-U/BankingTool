import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MutualFundsService } from '../../core/service/mutual-funds.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { UserInvestmentProfileDto } from '../../dto/user-investment-profile-dto';

@Component({
  selector: 'app-investment-profile',
  templateUrl: './investment-profile.component.html',
  styleUrls: ['./investment-profile.component.css']
})
export class InvestmentProfileComponent implements OnInit {
  investmentProfiles: UserInvestmentProfileDto[] = [];
  totalInvested: number = 0;
  totalCurrentValue: number = 0;
  totalUnrealisedGain: number = 0;
  totalRealisedGain: number = 0;
  overallAbsoluteReturn: number = 0;

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
    this.calculateTotals();
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

    // Calculate overall absolute return percentage
    if (this.totalInvested > 0) {
      this.overallAbsoluteReturn = ((this.totalCurrentValue - this.totalInvested) / this.totalInvested) * 100;
    }
  }

  getReturnClass(value: number): string {
    if (value > 0) {
      return 'positive';
    } else if (value < 0) {
      return 'negative';
    }
    return 'neutral';
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
