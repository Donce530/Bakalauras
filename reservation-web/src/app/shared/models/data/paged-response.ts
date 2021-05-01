import { Paginator } from "./paginator";

export class PagedResponse<T> {
    public paginator: Paginator;
    public results: T[];
}
