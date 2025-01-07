import { PlatformLocation } from '@angular/common';
import { Injectable } from '@angular/core';
import { IntegrateService } from './integrate.service';
import * as _ from 'lodash';
import { AppSettingsDto } from '../../dto/app-settings-dto';

@Injectable({
  providedIn: 'root'
})
export class InitializerService {
  constructor(
    private _platformLocation: PlatformLocation,
    private _appConfig: IntegrateService
  ) { }
  public stripTrailingSlash(value: string){
    return value.replace(/\/$/, "");
  }
  
  init(): () => Promise<boolean> {
    return () => {
      return new Promise<boolean>((resolve, reject) => {
        let appBaseUrl = this.stripTrailingSlash(this.getDocumentOrigin() + this.getBaseHref());
        console.log('appBaseUrl: ' + appBaseUrl);
        this._appConfig.getAppConfiguration().subscribe((response: any) => {
          _.merge(AppSettingsDto, response);
          resolve(true);
        });
      });
    };
  }
  
  private getBaseHref(): string {
    let baseUrl = this._platformLocation.getBaseHrefFromDOM();
  
    if (baseUrl && baseUrl == '/') {
      baseUrl = ''
    }
  
    return baseUrl;
  }
  
  private getDocumentOrigin(): string {
    if (!document.location.origin) {
      const port = document.location.port ? ':' + document.location.port : '';
      return (
        document.location.protocol + '//' + document.location.hostname + port
      );
    }
  
    return document.location.origin;
  }
}

