export interface MutualFundsDto {
  schemeCode: string;
  isinDivPayout: string;
  isinGrowth: string;
  isinDivReinvestment: string;
  schemeName: string;
  mutualFundValue: number;
  mutualFundDate: Date;
}

export interface UserInvestmentDto {
  investmentId: number;
  userId: number;
  schemeId: number;
  schemeName?: string;
  installmentAmount: number;
  unitsPurchased: number;
  purchaseDate: Date;
  mutualFundAtPurchase: number;
  schemeCode?: string;
  mutualFundCurrentValue: number;
}

export interface InsertUserInvestmentRequest {
  schemeCodes: string[];
  installmentAmount: number;
}

export interface MutualFundsListResponse {
  mutualFundsList: MutualFundsDto[];
}

export interface UserInvestmentListResponse {
  userInvestmentList: UserInvestmentDto[];
}

export interface MutualFundsInitialLoadResponse {
  mutualFundsDbList: MutualFundsDto[];
  userInvestmentList: UserInvestmentDto[];
}
