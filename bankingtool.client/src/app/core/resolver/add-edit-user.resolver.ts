import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { GetUserInitialLoadRequestObject } from '../../dto/request/user/get-user-initial-load-request';
import { UserService } from '../service/user.service';

@Injectable({
  providedIn: 'root'
})
export class AddEditUserResolver implements Resolve<any> {
  constructor(private userService: UserService) { }
  resolve(): Observable<any> {
    const model: GetUserInitialLoadRequestObject = {
      request: {
        user: {
          id: null
        }
      }
    };
    return this.userService.getUserInitialLoad(model).pipe(
      catchError((error) => { return of(error) }));
  }
}
