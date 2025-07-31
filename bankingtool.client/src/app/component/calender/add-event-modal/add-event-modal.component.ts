import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { CalendarEventDto } from '../../../dto/request/calender/calendar-event-dto';

@Component({
  selector: 'app-add-event-modal',
  templateUrl: './add-event-modal.component.html',
  styleUrls: ['./add-event-modal.component.css']
})
export class AddEventModalComponent {
  now = new Date();
  localDate = this.now.toLocaleDateString('en-CA');
  pad = (n: number) => n.toString().padStart(2, '0');
  localDateTime = `${this.localDate}T${this.pad(this.now.getHours())}:${this.pad(this.now.getMinutes())}`;

  newEvent: Partial<CalendarEventDto & { recurrenceFreq?: string; interval?: number; startDate?: string; endDate?: string }> = {
    isAllDay: false,
    userId: 0,
    eventType: 'event',
    startDate: this.localDate,
    startDateTime: this.localDateTime
  };

  constructor(public dialogRef: MatDialogRef<AddEventModalComponent>) { }

  onSubmit(): void {
    let recurrence: string | null = null;
    if (this.newEvent.recurrenceFreq && this.newEvent.interval) {
      recurrence = `FREQ=${this.newEvent.recurrenceFreq};INTERVAL=${this.newEvent.interval}`;
      if (this.newEvent.endDateTime && !this.newEvent.isAllDay) {
        const untilDate = new Date(this.newEvent.endDateTime);
        const untilString = `${untilDate.getFullYear()}${String(untilDate.getMonth() + 1).padStart(2, '0')}${String(untilDate.getDate()).padStart(2, '0')}T${String(untilDate.getHours()).padStart(2, '0')}${String(untilDate.getMinutes()).padStart(2, '0')}${String(untilDate.getSeconds()).padStart(2, '0')}`;
        recurrence += `;UNTIL=${untilString}`; // e.g., FREQ=DAILY;INTERVAL=2;UNTIL=20250830T180000
      }
    }
    const event: CalendarEventDto = {
      calendarEventId: 0,
      userId: 0,
      title: this.newEvent.title || 'New Event',
      description: this.newEvent.description || 'No description provided',
      startDateTime: this.newEvent.isAllDay
        ? this.newEvent.startDate || new Date().toISOString().split('T')[0]
        : this.newEvent.startDateTime || '2025-07-31T18:00',
      endDateTime: this.newEvent.isAllDay ? this.newEvent.endDate || null : this.newEvent.endDateTime || null,
      eventType: this.newEvent.eventType || 'event',
      recurrence,
      duration: this.newEvent.isAllDay ? null : (this.newEvent.endDateTime ? null : 'PT1H'),
      isAllDay: this.newEvent.isAllDay || false
    };
    this.dialogRef.close(event);
  }

  onCancel(): void {
    this.dialogRef.close(); // Close without data
  }
}
