import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IntegrateService {
  private apiBaseUrl = 'https://localhost:7113';

  constructor(private http: HttpClient) { }

  getAppConfiguration(): Observable<any> {
    return this.http.get<any>(`${this.apiBaseUrl}/AppSettings`, { 
      headers: this.getWebAppApiHeaders(),
      withCredentials: false 
    });
  }

  getResponse(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiBaseUrl}/AppSettings`, data, { 
      headers: this.getWebAppApiHeaders(),
      withCredentials: false 
    });
  }

  getWebAppApiHeaders() {
    let headers = new HttpHeaders();
    headers = headers.append('skip', 'true');
    headers = headers.append('Content-Type', 'application/json');
    return headers;
  }
}
