import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { AppSettingsDto } from '../../dto/app-settings-dto';
import { EmitterService } from '../emitter.service';
import { LocalStorageService } from '../local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  baseUrl = AppSettingsDto.baseUrl;
  constructor(private localStoreService: LocalStorageService, private emitService: EmitterService) { }
  requestObj = (method: string = 'GET', headerType: string = 'DEFAULT', body?: any) => {
    return {
      method,
      headerType,
      body
    }
  }

  get(url: string, isLoading: boolean = true): Observable<any> {
    return this.executeRequest(url, this.requestObj(methods.GET, methods.GET), isLoading);
  }

  post(url: string, body: any, isLoading: boolean = true): Observable<any> {
    return this.executeRequest(url, this.requestObj(methods.POST, methods.POST, JSON.stringify(body)), isLoading);
  }

  executeRequest(url: string, requestObj: any, isLoading: boolean): Observable<any> {
    return new Observable((observer: Observer<any>) => {
      let requestInit = this.getFetchObject(requestObj);
      if (isLoading) {
        this.emitService.loaderEmitter.emit(true);
      }
      console.log(this.baseUrl + url);
      fetch(this.baseUrl + url, requestInit)
        .then((response: any) => {
          return this.handelResponse(response);
        })
        .then(result => {
          if (!result.status) {
            //alart
          }
          if (isLoading) {
            this.emitService.loaderEmitter.emit(false);
          }
          observer.next(result);
          observer.complete();
        })
        .catch(error => {
          observer.error(error);
          observer.complete();
        });
    })
  }

  getFetchObject(requestObj: any) {
    let headers;
    switch (requestObj.headerType) {
      case methods.POST: {
        headers = this.postHeaders()
        break;
      }
      case methods.GET: {
        headers = this.getHeaders()
        break;
      }
      default: {
        headers = this.getHeaders()
      }
    }
    let request: any = {
      method: requestObj.method, // *GET, POST, PUT, DELETE, etc.
      headers,
      redirect: 'follow',
      referrerPolicy: 'no-referrer',
    };
    if (requestObj.body) {
      request.body = requestObj.body;
    }
    return request;
  }

  getBaseHeaders() {
    const token = this.localStoreService.getItem('accessToken');
    let base: any = {};
    if (token) {
      base['Authorization'] = `Bearer ${token}`
    }
    if (AppSettingsDto.apiKey) {
      base['ApiKey'] = AppSettingsDto.apiKey;
    }
    return base;
  }

  getHeaders() {
    let base: any = this.getBaseHeaders();
    base.Accept = 'application/json';
    base.Pragma = 'no-cache';
    base['Cache-Control'] = 'no-cache';
    return base;
  }
  postHeaders() {
    let base: any = this.getBaseHeaders();
    base.Accept = 'application/json';
    base['Content-Type'] = 'application/json';
    return base;
  }

  handelResponse(response: any) {
    if (response.status == 403) {
      return response.json();
    } else if (!response || response.status === 204) {
      return;
    } else if (response && response.status >= 401) {
      this.emitService.loaderEmitter.emit(false);
      throw response;
    } else if (response && response.status >= 500) {
      this.emitService.loaderEmitter.emit(false);
      throw response;
    } else {
      return response.json()
    }
  }
}

export class methods {
  public static readonly GET = "GET";
  public static readonly POST = "POST";
  public static readonly PUT = "PUT";
  public static readonly DELETE = "DELETE";
}
