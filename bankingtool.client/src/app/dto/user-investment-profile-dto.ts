export interface UserInvestmentProfileDto {
  investmentId: number;
  schemeId: number;
  schemeCode: string;
  schemeName: string;
  totalUnitsPurchased: number;
  investmentCost: number;
  currentValue: number;
  unrealisedGain: number;
  realisedGain: number;
  absoluteReturnPercent: number;
  xirrPercent: number;
  oneDayChange: number;
  investedDate: Date
}
