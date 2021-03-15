import { Role } from "../enums/role.enum";

export class User {
    id: number;
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    role: Role;
    token?: string;
}

