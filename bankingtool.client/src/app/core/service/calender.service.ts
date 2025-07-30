import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class CalenderService {
  constructor(private httpService: HttpService) { }

  getCalendarEventList(): Observable<any> {
    return this.httpService.get('Calender/GetCalendarEventList');
  }
}
