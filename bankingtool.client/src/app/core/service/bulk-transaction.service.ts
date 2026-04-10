import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { BulkTransactionRequestObject } from '../../dto/request/bulkTransaction/bulk-transaction-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class BulkTransactionService {
  constructor(private httpService: HttpService) { }

  getBulkTransactionInitialLoad(): Observable<ResponseDto<boolean>> {
    return this.httpService.get('BulkTransaction/GetBulkTransactionInitialLoad');
  }

  submitTransactions(payload: BulkTransactionRequestObject): Observable<ResponseDto<boolean>> {
    return this.httpService.post('BulkTransaction/BulkTransaction', payload);
  }
}
