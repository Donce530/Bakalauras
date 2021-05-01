import { Role } from "src/app/shared/enums/role.enum";

export class UserDataRow {
    public id: number;
    public firstName: string;
    public lastName: string;
    public email: string;
    public phoneNumber: string;
    public role: Role;
}
