import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private httpService: HttpService) { }

  login(loginData: any): Observable<any> {
    return this.httpService.get(`User/Login?email=${loginData.email}&password=${loginData.password}`);
  }
  createToken(userData: any): Observable<any> {
    return this.httpService.post(`User/CreateToken`, userData);
  }
}
