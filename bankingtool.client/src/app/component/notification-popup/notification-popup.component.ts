import { Component } from '@angular/core';
import { NotificationData, NotificationService } from '../../core/notificationService/notification-service';

@Component({
  selector: 'app-notification-popup',
  templateUrl: './notification-popup.component.html',
  styleUrls: ['./notification-popup.component.css']
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
