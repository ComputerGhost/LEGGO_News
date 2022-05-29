import { User } from "oidc-client-ts";
import UserRoles from "../constants/UserRoles";

export default class {
    private user: User | null;

    constructor(user: User | null) {
        this.user = user;
    }

    public hasAnyRole(targetRoles: string[]): boolean {
        if (this.user == null) {
            throw new Error("User is not signed in.");
        }

        const userRoles = this.normalizeRoles(this.user.profile.roles as string | string[]);

        return userRoles.some(role =>
            role == UserRoles.Administrator
            || targetRoles.includes(role)
        );
    }

    public hasRole(targetRole: string | string[]): boolean {
        var targetRoles = this.normalizeRoles(targetRole);
        return this.hasAnyRole(targetRoles);
    }


    private normalizeRoles(source: string | string[]): string[] {
        return (typeof source == "string")
            ? [source]
            : source;
    }
}