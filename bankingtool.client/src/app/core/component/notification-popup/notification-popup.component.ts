import { animate, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { NotificationData, NotificationService } from '../../notificationService/notification-service';

@Component({
  selector: 'app-notification-popup',
  templateUrl: './notification-popup.component.html',
  styleUrls: ['./notification-popup.component.css'],
  animations: [
    trigger('toastAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('300ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({ opacity: 0, transform: 'translateY(20px)' }))
      ])
    ])
  ]
})
export class NotificationPopupComponent {
  notifications: NotificationData[] = [];

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationService.notification$.subscribe((data: NotificationData) => {
      const notification = { ...data, id: data.id ?? Date.now() };
      this.notifications.push(notification);

      setTimeout(() => {
        this.notifications = this.notifications.filter(n => n.id !== notification.id);
      }, data.duration || 5000);
    });
  }

  getAlertClass(status: string) {
    switch (status) {
      case 'success': return 'alert-success';
      case 'error': return 'alert-danger';
      case 'info': return 'alert-info';
      case 'warning': return 'alert-warning';
      default: return 'alert-secondary';
    }
  }

  dismiss(id: number | undefined) {
    this.notifications = this.notifications.filter(n => n.id !== id);
  }
}
