import { Paginator } from 'src/app/shared/models/data/paginator';
import { UserFilters } from './user-filters';

export class UsersRequest {
    public paginator: Paginator;
    public filters: UserFilters;

    constructor(data?: Partial<UsersRequest>) {
        Object.assign(this, data);
    }
}
