import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, forkJoin } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { BulkTransactionService } from '../service/bulk-transaction.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';

@Injectable({
  providedIn: 'root'
})
export class BulkTransactionResolver implements Resolve<boolean> {
  constructor(private bulkTransactionService: BulkTransactionService) { }
  resolve(): Observable<any> {
    return this.bulkTransactionService.getBulkTransactionInitialLoad().pipe(
      catchError((error) => { return of(error) }));
  }
}
