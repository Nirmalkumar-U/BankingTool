import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetBudgetPlannerListResponse, GetBudgetPlannerDetailResponse } from '../../dto/response/get-budget-planner-response';

@Injectable({
  providedIn: 'root'
})
export class BudgetPlannerService {
  private readonly apiController = 'BudgetPlanner';

  constructor(private httpService: HttpService) { }

  createBudgetPlanner(request: any): Observable<ResponseDto<boolean>> {
    return this.httpService.post(
      `${this.apiController}/CreateBudgetPlanner`,
      request
    );
  }

  updateBudgetPlanner(request: any): Observable<ResponseDto<boolean>> {
    return this.httpService.post(
      `${this.apiController}/UpdateBudgetPlanner`,
      request
    );
  }

  deleteBudgetPlanner(budgetPlannerId: number): Observable<ResponseDto<boolean>> {
    return this.httpService.post(
      `${this.apiController}/DeleteBudgetPlanner`,
      { budgetPlannerId: budgetPlannerId }
    );
  }

  getBudgetPlannerList(): Observable<ResponseDto<GetBudgetPlannerListResponse>> {
    return this.httpService.get(
      `${this.apiController}/GetBudgetPlannerList`
    );
  }

  getBudgetPlannerDetail(budgetPlannerId: number): Observable<ResponseDto<GetBudgetPlannerDetailResponse>> {
    return this.httpService.post(
      `${this.apiController}/GetBudgetPlannerDetail`,
      { budgetPlannerId: budgetPlannerId }
    );
  }
}
