import { ChangeDetectorRef, Component } from '@angular/core';
import { CalendarOptions, EventClickArg, EventMountArg } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import listPlugin from '@fullcalendar/list';
import timeGridPlugin from '@fullcalendar/timegrid';
import rrulePlugin from '@fullcalendar/rrule'
import { CalenderService } from '../../core/service/calender.service';
import { CalendarEventDto } from '../../dto/request/calender/calendar-event-dto';
import { ResponseDto } from '../../dto/response-dto';

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
    initialView: 'dayGridMonth',
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

  constructor(private calendarService: CalenderService, private cdr: ChangeDetectorRef) { }

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
    // Example: Add a new event (customize based on your form/modal)
    const today = new Date();
    const plus7Days = new Date();
    plus7Days.setDate(today.getDate() + 7);
    const newEvent: CalendarEvent = {
      id: Math.random().toString(),
      title: 'New Event',
      start: new Date().toISOString(),
      end: plus7Days.toISOString(),
      extendedProps: {
        type: 'event',
        description: 'Default description for new event'
      }
    };
    this.sampleEvents.push(newEvent);
    this.loadEvents();
    //this.calendarService.addEvent(newEvent).subscribe(() => {
    //  this.loadEvents();
    //});
  }


  private sampleEvents: CalendarEvent[] = [];
  
}
