export class SnappingSettings {
    public scale: number;
    public enabled: boolean;
    public available: boolean;

    constructor(data?: Partial<SnappingSettings>) {
        Object.assign(this, data);
    }
}
