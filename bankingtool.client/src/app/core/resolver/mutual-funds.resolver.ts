import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { MutualFundsService } from '../service/mutual-funds.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { MutualFundsDto, UserInvestmentDto, MutualFundsInitialLoadResponse } from '../../dto/mutual-funds-dto';
import { catchError, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MutualFundsResolver implements Resolve<boolean> {
  constructor(private mutualFundsService: MutualFundsService) { }

  resolve(): Observable<any> {
    return this.mutualFundsService.getAllMutualFunds().pipe(
      catchError((error) => { return of(error) }));
  }
}
