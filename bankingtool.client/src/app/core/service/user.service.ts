import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTokenRequestObject } from '../../dto/request/user/create-token-request';
import { GetCityListRequestObject } from '../../dto/request/user/get-city-list-request';
import { GetUserInitialLoadRequestObject } from '../../dto/request/user/get-user-initial-load-request';
import { LoginRequestObject } from '../../dto/request/user/login-request';
import { SaveUserRequestObject } from '../../dto/request/user/save-user-request';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private httpService: HttpService) { }

  login(model: LoginRequestObject): Observable<any> {
    return this.httpService.post('User/Login', model);
  }
  createToken(model: CreateTokenRequestObject): Observable<any> {
    return this.httpService.post('User/CreateToken', model);
  }
  getUserInitialLoad(model: GetUserInitialLoadRequestObject): Observable<any> {
    return this.httpService.post('User/GetUserInitialLoad', model);
  }
  getCityDropDownListByStateId(model: GetCityListRequestObject): Observable<any> {
    return this.httpService.post('User/getCityDropDownListByStateId', model);
  }
  saveUser(model: SaveUserRequestObject): Observable<any> {
    return this.httpService.post('User/SaveUser', model);
  }
  getUserList(): Observable<any> {
    return this.httpService.post('User/GetUserList', null);
  }
}
