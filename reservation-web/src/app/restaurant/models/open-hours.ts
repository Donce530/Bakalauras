import { WeekDay } from '@angular/common';

export class OpenHours {
    public weekDay: WeekDay;
    public open: Date;
    public close: Date;

    constructor(data: Partial<OpenHours>) {
        Object.assign(this, data);
    }

    static defaultSchedule(): OpenHours[] {
        const open = new Date();
        const timezoneOffset = open.getTimezoneOffset() / 60;
        open.setUTCHours(9 + timezoneOffset, 0);
        const close = new Date();
        close.setUTCHours(23 + timezoneOffset, 0);

        return [
            new OpenHours({ weekDay: WeekDay.Monday, open, close }),
            new OpenHours({ weekDay: WeekDay.Tuesday, open, close }),
            new OpenHours({ weekDay: WeekDay.Wednesday, open, close }),
            new OpenHours({ weekDay: WeekDay.Thursday, open, close }),
            new OpenHours({ weekDay: WeekDay.Friday, open, close }),
            new OpenHours({ weekDay: WeekDay.Saturday, open, close }),
            new OpenHours({ weekDay: WeekDay.Sunday, open, close }),
        ];
    }

    public get writeableDay(): string {
        switch (this.weekDay) {
            case WeekDay.Monday: {
                return 'Pirmadienis';
            }
            case WeekDay.Tuesday: {
                return 'Antradienis';
            }
            case WeekDay.Wednesday: {
                return 'Trečiadienis';
            }
            case WeekDay.Thursday: {
                return 'Ketvirtadienis';
            }
            case WeekDay.Friday: {
                return 'Penktadienis';
            }
            case WeekDay.Saturday: {
                return 'Šeštadienis';
            }
            case WeekDay.Sunday: {
                return 'Sekmadienis';
            }
        }
    }
}
