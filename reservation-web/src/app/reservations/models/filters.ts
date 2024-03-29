export class Filters {
    public name: string;
    public day: Date;
    public startAfter: Date;
    public startUntil: Date;
    public endAfter: Date;
    public endUntil: Date;
    public realStartAfter: Date;
    public realStartUntil: Date;
    public realEndAfter: Date;
    public realEndUntil: Date;
    public tableNumber: number;

    constructor(data?: Partial<Filters>) {
        Object.assign(this, data);
    }
}
