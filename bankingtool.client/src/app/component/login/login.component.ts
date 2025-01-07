import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { forEach } from 'lodash';
import { AppPaths, ClaimKey, Constant } from '../../constant/constant';
import { LoginService } from '../../core/apiService/login.service';
import { EmitterService } from '../../core/emitter.service';
import { IndexedDbServiceService } from '../../core/indexedDBService/indexed-db-service.service';
import { LocalStorageService } from '../../core/local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm = this.formBuilder.group({
    email: [null, [Validators.required]],
    password: [null, [Validators.required]],
  });

  constructor(private loginService: LoginService, private router: Router,
    private localStoreService: LocalStorageService, private emitService: EmitterService,
    private formBuilder: FormBuilder, private indexedDbService: IndexedDbServiceService) {
  }

  login() {
    if (!this.loginForm.invalid) {
      const loginData = this.loginForm.value;
      this.loginService.login(loginData).subscribe((response: any) => {
        if (response.status) {
          this.loginService.createToken(response.result).subscribe((result: any) => {
            if (result.status) {
              this.localStoreService.clear();
              this.localStoreService.setAllData(result.result.claims);
              this.localStoreService.setItem(ClaimKey.isLoggedIn, Constant.true);
              this.localStoreService.setItem(ClaimKey.accessToken, result.result.accessToken);
              this.indexedDbService.initDb(Constant.actionDBName, Constant.dbVersion,
                Constant.actionTablePrimaryKey, Constant.actionTableName, Constant.actionCoumns).then(() => {
                  result.result.actionPaths.forEach((data: any) => {
                    this.indexedDbService.addData(Constant.actionTableName, data);
                  });
                  this.emitService.loginEmitter.emit(true);
                  this.router.navigate([AppPaths.home]);
                });
            }
          });
        }
      });
    }
  }
}
