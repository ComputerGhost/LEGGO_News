import RestApi from '../internal/HookedApi';

export interface CalendarDetails {
    id: number,
    color: string,
    googleId: string,
    name: string,
    timezoneOffset: number,
}

export interface CalendarSaveData {
    color: string,
    googleId: string,
    name: string,
    timezoneOffset: number,
}

export interface CalendarSummary {
    id: number,
    color: string,
    name: string,
}

const calendars = new RestApi<CalendarSummary, CalendarDetails, CalendarSaveData>('calendars');

export function useCalendars(search: string) {
    return calendars.useItems(search);
}

export function useCalendar(calendarId: number | undefined) {
    return calendars.useItem(calendarId);
}

export function useCreateCalendar() {
    return calendars.useCreateItem();
}

export function useUpdateCalendar(calendarId: number | undefined) {
    if (!calendarId) {
        throw new Error('calendarId must be defined to update.');
    }
    return calendars.useUpdateItem(calendarId);
}

export function useDeleteCalendar(calendarId: number) {
    return calendars.useDeleteItem(calendarId);
}
