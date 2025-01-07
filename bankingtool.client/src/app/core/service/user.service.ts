import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SaveUserDto } from '../../dto/save-user-dto';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private httpService: HttpService) { }

  getUserInitialLoad(userId: number | null): Observable<any> {
    return this.httpService.get('User/GetUserInitialLoad?userId=' + (userId == null ? '' : userId));
  }
  getCityDropDownListByStateId(stateId: number): Observable<any> {
    return this.httpService.get('User/getCityDropDownListByStateId?stateId=' + stateId);
  }
  saveUser(user: SaveUserDto): Observable<any> {
    return this.httpService.post('User/SaveUser', user);
  }
}
