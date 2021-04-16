export class InputTextParameters {
    public x: number;
    public y: number;

    constructor(data?: Partial<InputTextParameters>) {
        Object.assign(this, data);
    }
}