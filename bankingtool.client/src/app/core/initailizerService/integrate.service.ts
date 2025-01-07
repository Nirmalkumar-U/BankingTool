import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IntegrateService {
  constructor(private http: HttpClient) { }

  getAppConfiguration(): Observable<any> {
    return this.http.get<any>('https://localhost:7113/AppSettings', { headers: this.getWebAppApiHeaders() });
  }
  getWebAppApiHeaders() {
    let headers = new HttpHeaders();
    headers = headers.append('skip', 'true');
    return headers;
  }
}
