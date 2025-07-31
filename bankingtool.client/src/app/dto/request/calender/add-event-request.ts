import { CalendarEventDto } from "./calendar-event-dto";

export interface AddEventRequestObject {
  request: AddEventRequest;
}

export interface AddEventRequest {
  calendarEvent: CalendarEventDto;
}


export const CreateAddEventRequest = (request: CalendarEventDto): AddEventRequestObject => {
  let model: AddEventRequestObject = {
    request: {
      calendarEvent: {
        calendarEventId: 0,
        title: request.title,
        description: request.description,
        duration: request.duration,
        userId: 0,
        eventType: request.eventType,
        isAllDay: request.isAllDay,
        endDateTime: request.endDateTime,
        startDateTime: request.startDateTime,
        recurrence: request.recurrence
      }
    }
  }
  return model;
}
