import { RouteObject } from "react-router-dom";

export default interface Module {
    routes: RouteObject[],
    name: string,
    icon: string,
};