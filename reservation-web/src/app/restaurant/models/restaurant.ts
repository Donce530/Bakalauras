import { OpenHours } from './open-hours';

export class Restaurant {
    public title: string;
    public description: string;
    public schedule: OpenHours[];
    public address: string;
    public city: string;

    constructor(data: Partial<Restaurant>) {
        Object.assign(this, data);
    }
}
