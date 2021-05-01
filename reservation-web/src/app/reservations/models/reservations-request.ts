import { Filters } from './filters';
import { Paginator } from '../../shared/models/data/paginator';

export class ReservationsRequest {
    public paginator: Paginator;
    public filters: Filters;

    constructor(data?: Partial<ReservationsRequest>) {
        Object.assign(this, data);
    }
}
