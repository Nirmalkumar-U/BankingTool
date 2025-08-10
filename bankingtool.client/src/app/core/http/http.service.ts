import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Observer } from 'rxjs';
import { AppSettingsDto } from '../../dto/internal-dto/app-settings-dto';
import { CreateTokenRequestObject } from '../../dto/request/user/create-token-request';
import { EmitterService } from '../emitter.service';
import { LocalStorageService } from '../local-storage.service';
import { NotificationService } from '../notificationService/notification-service';
import { AuthService } from '../service/auth.service';
import { LoaderService } from '../service/loader.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  baseUrl = AppSettingsDto.baseUrl;
  constructor(private localStoreService: LocalStorageService, private emitService: EmitterService,
    private notificationService: NotificationService, private authService: AuthService, private loaderService: LoaderService
  ) { }
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
      const makeFetch = async (retry = false): Promise<void> => {
        try {
          let requestInit = this.getFetchObject(requestObj);
          if (isLoading) {
            this.loaderService.show();
          }

          const response = await fetch(this.baseUrl + url, requestInit);

          if (response.status === 498 && !retry) {
            const success = await this.refreshToken();
            if (success) {
              await makeFetch(true);
              return;
            } else {
              throw new Error("Token refresh failed");
            }
          }

          const result = await this.handelResponse(response);

          if (isLoading) {
            this.loaderService.hide();
          }

          if (result === null || result === undefined) {
            this.notificationService.show("Internal server error", 'error');
          }

          observer.next(result);
          observer.complete();
        } catch (error) {
          if (isLoading) {
            this.loaderService.hide();
          }
          observer.error(error);
          observer.complete();
        }
      };

      makeFetch();
    });
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
      this.loaderService.hide();
      this.notificationService.show("Session expired...", 'error');
      this.authService.logout();
      throw response;
    } else if (response && response.status >= 500) {
      this.loaderService.hide();
      this.notificationService.show("Internal server error " + response.status.toString(), 'error');
      throw response;
    } else {
      return response.json()
    }
  }

  private refreshToken(): Promise<boolean> {
    let roleId = localStorage.getItem('RoleId');
    let emailId = localStorage.getItem('EmailId');
    let userId = localStorage.getItem('UserId');
    let userName = localStorage.getItem('UserName');
    let name: string[] = userName?.split(' ')!;
    let firstName = name[0];
    let lastName = name[1];
    let request: CreateTokenRequestObject = {
      request: {
        role: {
          id: Number(roleId)
        },
        user: {
          id: Number(userId),
          email: emailId!,
          firstName: firstName,
          lastName: lastName
        }
      }
    }

    const requestObj = this.requestObj(methods.POST, methods.POST, JSON.stringify(request));
    const requestInit = this.getFetchObject(requestObj);

    return fetch(this.baseUrl + 'User/CreateToken', requestInit)
      .then(res => res.json())
      .then(data => {
        if (data && data.response.accessToken) {
          this.localStoreService.setItem('accessToken', data.response.accessToken);
          return true;
        }
        return false;
      })
      .catch(() => false);
  }
}

export class methods {
  public static readonly GET = "GET";
  public static readonly POST = "POST";
  public static readonly PUT = "PUT";
  public static readonly DELETE = "DELETE";
}
