import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export type NotificationStatus = 'success' | 'error' | 'info' | 'warning';

export interface NotificationData {
  id?: number;
  message: string;
  status: NotificationStatus;
  duration?: number; // in milliseconds
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationSubject = new Subject<NotificationData>();
  notification$ = this.notificationSubject.asObservable();

  private counter = 0;

  show(message: string, status: NotificationStatus = 'info', duration: number = 5000) {
    this.notificationSubject.next({
      id: ++this.counter,
      message,
      status,
      duration
    });
  }
}


