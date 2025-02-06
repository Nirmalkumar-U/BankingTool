import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { UserService } from '../service/user.service';

@Injectable({
  providedIn: 'root'
})
export class AddEditUserResolver implements Resolve<any> {
  //resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
  //  return of(true);
  //}

  constructor(private userService: UserService) { }
  resolve(): Observable<any> {
    return this.userService.getUserInitialLoad(null).pipe(
      catchError((error) => { return of(error) }));
  }
}
