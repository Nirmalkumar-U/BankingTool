export interface CalendarEventDto {
  calendarEventId: number;
  userId: number;
  title: string;
  description: string;
  startDateTime: string;
  endDateTime: string | null;
  eventType: 'meeting' | 'reminder' | 'notes' | 'event' | 'holiday';
  recurrence: string;
  duration: string;
  isAllDay: boolean;
}
