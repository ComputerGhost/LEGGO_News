"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.useAuth = void 0;
var react_cookie_1 = require("react-cookie");
function useAuth() {
    var cookies = react_cookie_1.useCookies(['oidc-user'])[0];
    var userCookie = cookies['oidc-user'];
    return userCookie;
}
exports.useAuth = useAuth;
//# sourceMappingURL=useAuth.js.map