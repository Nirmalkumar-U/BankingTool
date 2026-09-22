import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { MutualFundsService } from '../service/mutual-funds.service';

@Injectable({
  providedIn: 'root'
})
export class InvestmentProfileResolver implements Resolve<any> {

  constructor(private mutualFundsService: MutualFundsService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    return this.mutualFundsService.getUserInvestmentProfile();
  }
}
