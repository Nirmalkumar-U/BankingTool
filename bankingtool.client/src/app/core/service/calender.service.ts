import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddEventRequestObject } from '../../dto/request/calender/add-event-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class CalenderService {
  constructor(private httpService: HttpService) { }

  getCalendarEventList(): Observable<any> {
    return this.httpService.get('Calender/GetCalendarEventList');
  }
  addEvent(model: AddEventRequestObject): Observable<any> {
    return this.httpService.post('Calender/AddEvent', model);
  }
}
