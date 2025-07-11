import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { LocalStorageService } from '../local-storage.service';
import { IndexedDbServiceService } from '../indexedDBService/indexed-db-service.service';
import { AppPaths, Constant } from '../../constant/constant';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginState = new BehaviorSubject<boolean>(true);
  isLogined$ = this.loginState.asObservable();

  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
    private indexedDbService: IndexedDbServiceService
  ) { }

  setLoginState(state: boolean) {
    this.loginState.next(state);
  }

  logout(): void {
    this.setLoginState(false);
    this.localStorageService.clear();

    this.indexedDbService.initDb(
      Constant.actionDBName,
      Constant.dbVersion,
      Constant.actionTablePrimaryKey,
      Constant.actionTableName,
      Constant.actionCoumns
    ).then(() => {
      this.indexedDbService.deleteDatabase(Constant.actionTableName).then(() => { });
      this.indexedDbService.clearData(Constant.actionTableName).then(() => { });
    });
    this.router.navigate([AppPaths.login]);
  }
}
