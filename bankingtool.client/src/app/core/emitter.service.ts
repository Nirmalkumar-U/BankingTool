import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmitterService {
  loginEmitter: LoginEmitter;
  constructor() {
    this.loginEmitter = new LoginEmitter();
  }
}

export class LoginEmitter extends Subject<boolean>{
  constructor() {
    super();
  }
  emit(value: any) {
    super.next(value);
  }
}
