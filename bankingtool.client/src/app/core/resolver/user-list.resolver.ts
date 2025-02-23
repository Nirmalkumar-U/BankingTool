import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { UserService } from '../service/user.service';

@Injectable({
  providedIn: 'root'
})
export class UserListResolver implements Resolve<boolean> {
  constructor(private userService: UserService) { }
  resolve(): Observable<any> {
    return this.userService.getUserList().pipe(
      catchError((error) => { return of(error) }));
  }
}
