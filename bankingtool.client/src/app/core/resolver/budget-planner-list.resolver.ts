import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { BudgetPlannerService } from '../service/budget-planner.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetBudgetPlannerListResponse } from '../../dto/response/get-budget-planner-response';

@Injectable({
  providedIn: 'root'
})
export class BudgetPlannerListResolver implements Resolve<ResponseDto<GetBudgetPlannerListResponse>> {
  constructor(private budgetPlannerService: BudgetPlannerService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ResponseDto<GetBudgetPlannerListResponse>> {
    return this.budgetPlannerService.getBudgetPlannerList();
  }
}
