import { Injectable } from '@angular/core';
import { ClaimKey, Constant } from '../constant/constant';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  constructor() {
    if (!this.isLocalStorageAvailable()) {
      console.error("LocalStorage is not available in this environment.");
    }
  }

  private isLocalStorageAvailable(): boolean {
    try {
      const testKey = "__test__";
      localStorage.setItem(testKey, testKey);
      localStorage.removeItem(testKey);
      return true;
    } catch (e) {
      return false;
    }
  }

  getItem(key: string) {
    return localStorage.getItem(key);
  }
  setItem(key: string, value: string) {
    localStorage.setItem(key, value);
  }
  removeItem(key: string) {
    localStorage.removeItem(key);
  }
  clear() {
    localStorage.clear();
  }
  getIsLoggedIn() {
    return localStorage.getItem(ClaimKey.isLoggedIn) == Constant.true;
  }

  setIsLoggedIn(value: boolean) {
    localStorage.setItem(ClaimKey.isLoggedIn, value.toString().toLocaleLowerCase());
  }
  setAllData(data: any[]) {
    console.log(data);
    data.forEach((item) => {
      this.setItem(item.key, item.value)
    })
  }
}
