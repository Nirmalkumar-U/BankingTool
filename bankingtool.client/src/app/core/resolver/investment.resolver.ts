import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { MutualFundsService } from '../service/mutual-funds.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { MutualFundsDto, UserInvestmentDto, MutualFundsInitialLoadResponse } from '../../dto/mutual-funds-dto';

@Injectable({
  providedIn: 'root'
})
export class InvestmentResolver implements Resolve<ResponseDto<MutualFundsInitialLoadResponse>> {
  constructor(private mutualFundsService: MutualFundsService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return new Promise<ResponseDto<MutualFundsInitialLoadResponse>>((resolve) => {
      Promise.all([
        this.mutualFundsService.getAllMutualFundsInDB().toPromise(),
        this.mutualFundsService.getAllUserInvestment().toPromise()
      ]).then(([mutualFundsDBResponse, userInvestmentResponse]: any) => {
        const response: ResponseDto<MutualFundsInitialLoadResponse> = {
          status: mutualFundsDBResponse?.status && userInvestmentResponse?.status,
          message: 'Initial Load',
          response: {
            mutualFundsDbList: mutualFundsDBResponse?.response || [],
            userInvestmentList: userInvestmentResponse?.response || []
          },
          errors: [...(mutualFundsDBResponse?.errors || []), ...(userInvestmentResponse?.errors || [])],
          validationErrors: [...(mutualFundsDBResponse?.validationErrors || []), ...(userInvestmentResponse?.validationErrors || [])],
          dropDownList: [],
          statuCode: mutualFundsDBResponse?.statuCode || 0
        };
        resolve(response);
      }).catch((error) => {
        const errorResponse: ResponseDto<MutualFundsInitialLoadResponse> = {
          status: false,
          message: 'Error loading data',
          response: {
            mutualFundsDbList: [],
            userInvestmentList: []
          },
          errors: [{ errorMessage: error.message, propertyName: 'MutualFunds' }],
          validationErrors: [],
          dropDownList: [],
          statuCode: 500
        };
        resolve(errorResponse);
      });
    });
  }
}
