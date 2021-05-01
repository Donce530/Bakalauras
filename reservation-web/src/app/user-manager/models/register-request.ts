import { Role } from "src/app/shared/enums/role.enum";

export class RegisterRequest {
    public firstName: string;
    public lastName: string;
    public email: string;
    public phoneNumber: string;
    public role: Role;
}
