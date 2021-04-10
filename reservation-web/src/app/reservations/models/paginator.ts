export class Paginator {
    public rows: number;
    public offset: number;
    public sortBy: string;
    public sortOrder: number;
    public totalRows: number;

    constructor(data?: Partial<Paginator>) {
        Object.assign(this, data);
    }
}
