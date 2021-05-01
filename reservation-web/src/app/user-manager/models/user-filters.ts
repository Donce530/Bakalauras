import { Role } from 'src/app/shared/enums/role.enum';

export class UserFilters {
    public firstName: string;
    public lastName: string;
    public email: string;
    public phoneNumber: string;
    public role: Role;

    constructor(data?: Partial<UserFilters>) {
        Object.assign(this, data);
    }
}
