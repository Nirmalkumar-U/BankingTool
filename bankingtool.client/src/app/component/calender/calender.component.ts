import { ChangeDetectorRef, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CalendarOptions, EventClickArg, EventMountArg } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import listPlugin from '@fullcalendar/list';
import rrulePlugin from '@fullcalendar/rrule';
import timeGridPlugin from '@fullcalendar/timegrid';
import { NotificationService } from '../../core/notificationService/notification-service';
import { CalenderService } from '../../core/service/calender.service';
import { CreateAddEventRequest } from '../../dto/request/calender/add-event-request';
import { CalendarEventDto } from '../../dto/request/calender/calendar-event-dto';
import { ResponseDto } from '../../dto/response-dto';
import { AddEventModalComponent } from './add-event-modal/add-event-modal.component';

export interface CalendarEvent {
  id: string;
  title: string;
  start: string;
  end?: string;
  allDay?: boolean;
  rrule?: string | object;
  duration?: string;
  extendedProps: {
    type: 'meeting' | 'reminder' | 'notes' | 'event' | 'holiday';
    description?: string;
  };
}

@Component({
  selector: 'app-calender',
  templateUrl: './calender.component.html',
  styleUrls: ['./calender.component.css']
})
export class CalenderComponent {


  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, timeGridPlugin, listPlugin, rrulePlugin],
    initialView: 'timeGridWeek',
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    events: [],
    eventTimeFormat: {
      hour: 'numeric',
      minute: '2-digit',
      meridiem: 'short' // Displays 'am' or 'pm' instead of 'a' or 'p'
    },
    eventClick: this.handleEventClick.bind(this),
    eventDidMount: this.handleEventDidMount.bind(this) // Add eventDidMount to set colors dynamically
  };

  constructor(private calendarService: CalenderService, private cdr: ChangeDetectorRef,
    private dialog: MatDialog, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(): void {
    this.calendarService.getCalendarEventList().subscribe((response: ResponseDto<CalendarEventDto[]>) => {
      if (response.status && response.response) {
        let mappedEvents: CalendarEvent[] = response.response.map((dto: CalendarEventDto) => ({
          id: dto.calendarEventId.toString(),
          title: dto.title,
          start: dto.startDateTime,
          end: dto.endDateTime || undefined,
          allDay: dto.isAllDay,
          rrule: dto.recurrence || undefined,
          duration: dto.duration || undefined,
          extendedProps: {
            type: dto.eventType as 'meeting' | 'reminder' | 'notes' | 'event' | 'holiday',
            description: dto.description || undefined
          }
        }));
        this.calendarOptions.events = mappedEvents;
      } else {
        let messageList: string[] = [];
        if (Array.isArray(response?.errors) && response?.errors)
          messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
        messageList.push("Get Account Balance failed.");
        messageList.forEach((msg) => {
          this.notificationService.show(msg, 'error');
        });
      }
      this.cdr.detectChanges();
    });
  }

  handleEventClick(arg: EventClickArg): void {
    const event = arg.event;
    const eventDetails = `Title: ${event.title}\nType: ${event.extendedProps['type']}\nStart: ${event.start}\nEnd: ${event.end || 'N/A'}`;
    alert(eventDetails); // Replace with a modal or custom UI
  }

  handleEventDidMount(arg: EventMountArg): void {
    const eventType = arg.event.extendedProps['type'];
    let backgroundColor: string;

    switch (eventType) {
      case 'meeting':
        backgroundColor = '#3788d8'; // Blue
        break;
      case 'reminder':
        backgroundColor = '#ff9f89'; // Orange
        break;
      case 'notes':
        backgroundColor = '#28a745'; // Green
        break;
      case 'event':
        backgroundColor = '#6f42c1'; // Purple
        break;
      default:
        backgroundColor = '#3788d8';
    }

    arg.el.style.backgroundColor = backgroundColor;
    arg.el.style.borderColor = backgroundColor;
  }

  addEvent(): void {
    const dialogRef = this.dialog.open(AddEventModalComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe((result: CalendarEventDto) => {
      if (result) {
        let model = CreateAddEventRequest(result);
        this.calendarService.addEvent(model).subscribe((response: ResponseDto<number | null>) => {
          if (response.status == true) {
            this.notificationService.show("Event Added successfully", 'success');
            this.loadEvents()
          } else {
            let messageList: string[] = [];
            if (Array.isArray(response?.errors) && response?.errors)
              messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)]
            messageList.push("Get Account Balance failed.");
            messageList.forEach((msg) => {
              this.notificationService.show(msg, 'error');
            });
          }
        });
      }
    });
  }

}
