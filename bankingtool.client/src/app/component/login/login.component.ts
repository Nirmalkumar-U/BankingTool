import { Component, NgZone } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppPaths, ClaimKey, Constant } from '../../constant/constant';
import { EmitterService } from '../../core/emitter.service';
import { IndexedDbServiceService } from '../../core/indexedDBService/indexed-db-service.service';
import { LocalStorageService } from '../../core/local-storage.service';
import { NotificationService } from '../../core/notificationService/notification-service';
import { AuthService } from '../../core/service/auth.service';
import { LoaderService } from '../../core/service/loader.service';
import { UserService } from '../../core/service/user.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { CreateTokenRequestObject } from '../../dto/request/user/create-token-request';
import { LoginRequestObject } from '../../dto/request/user/login-request';
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
    private localStoreService: LocalStorageService, private emitService: EmitterService, private authService: AuthService,
    private formBuilder: FormBuilder, private indexedDbService: IndexedDbServiceService, private notificationService: NotificationService,
    private ngZone: NgZone) { }

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
        if (response.status == true) {
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
            if (result.status == true) {
              this.ngZone.run(() => {
                this.authService.setLoginState(true);
                this.localStoreService.setAllData(result.response.claims);
                this.localStoreService.setItem(ClaimKey.isLoggedIn, Constant.true);
                this.localStoreService.setItem(ClaimKey.accessToken, result.response.accessToken);

                this.indexedDbService.initDb(Constant.actionDBName, Constant.dbVersion,
                  Constant.actionTablePrimaryKey, Constant.actionTableName, Constant.actionCoumns).then(() => {

                    result.response.actionPaths.forEach((data: any) => {
                      this.indexedDbService.addData(Constant.actionTableName, data);
                    });
                    setTimeout(() => {
                      this.emitService.loginEmitter.emit(true);
                    }, 100); 

                    this.notificationService.show("Logged in successfully.", 'success');
                    this.router.navigate([AppPaths.home]);
                  });
              });
            } else {
              let messageList: string[] = [];
              if (Array.isArray(result?.errors) && result?.errors)
                messageList = [...result?.errors?.map(x => x.errorMessage), ...result?.validationErrors?.map(x => x.errorMessage)]
              messageList.push("Create Token failed.");
              messageList.forEach((msg) => {
                this.notificationService.show(msg, 'error');
              });
            }
          });
        } else {
          let messageList: string[] = [];
          if (Array.isArray(response?.errors) && response?.errors)
            messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
          messageList.push("Login failed");
          messageList.forEach((msg) => {
            this.notificationService.show(msg, 'error');
          });
        }
      });
    }
  }
}
