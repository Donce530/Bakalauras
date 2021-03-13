export class EditTableParameters {
    public index: number;
    public x: number;
    public y: number;

    constructor(data?: Partial<EditTableParameters>) {
        Object.assign(this, data);
    }
}
