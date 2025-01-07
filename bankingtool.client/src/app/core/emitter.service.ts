import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmitterService {
  loaderEmitter: LoaderEmitter;
  loginEmitter: LoginEmitter;
  constructor() {
    this.loaderEmitter = new LoaderEmitter();
    this.loginEmitter = new LoginEmitter();
  }
}

export class LoaderEmitter extends Subject<boolean>{
  constructor() {
    super();
  }
  emit(value: any) {
    super.next(value);
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
