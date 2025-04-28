import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppPaths, ClaimKey, Constant } from '../../constant/constant';
import { EmitterService } from '../../core/emitter.service';
import { IndexedDbServiceService } from '../../core/indexedDBService/indexed-db-service.service';
import { LocalStorageService } from '../../core/local-storage.service';
import { UserService } from '../../core/service/user.service';
import { CreateTokenRequestObject } from '../../dto/request/user/create-token-request';
import { LoginRequestObject } from '../../dto/request/user/login-request';
import { ResponseDto } from '../../dto/response-dto';
import { LoggedInUserResponse } from '../../dto/response/logged-in-user-response';
import { TokenResponse } from '../../dto/response/token-response';

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

  constructor(private loginService: UserService, private router: Router,
    private localStoreService: LocalStorageService, private emitService: EmitterService,
    private formBuilder: FormBuilder, private indexedDbService: IndexedDbServiceService) {
  }

  login() {
    if (!this.loginForm.invalid) {
      const loginData = this.loginForm.value;
      this.localStoreService.clear();
      let model: LoginRequestObject = {
        request: {
          user: {
            email: loginData.email!,
            password: loginData.password!
          }
        }
      };
      this.loginService.login(model).subscribe((response: ResponseDto<LoggedInUserResponse>) => {
        if (response.status) {
          let modelToken: CreateTokenRequestObject = {
            request: {
              role: {
                id: response.response.roleId
              },
              user: {
                email: response.response.email,
                firstName: response.response.firstName,
                lastName: response.response.lastName,
                id: response.response.userId
              }
            }
          }
          this.loginService.createToken(modelToken).subscribe((result: ResponseDto<TokenResponse>) => {
            if (result.status) {
              this.localStoreService.setAllData(result.response.claims);
              this.localStoreService.setItem(ClaimKey.isLoggedIn, Constant.true);
              this.localStoreService.setItem(ClaimKey.accessToken, result.response.accessToken);
              this.indexedDbService.initDb(Constant.actionDBName, Constant.dbVersion,
                Constant.actionTablePrimaryKey, Constant.actionTableName, Constant.actionCoumns).then(() => {
                  result.response.actionPaths.forEach((data: any) => {
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
