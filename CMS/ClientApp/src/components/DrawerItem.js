"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_dom_1 = require("react-router-dom");
var core_1 = require("@material-ui/core");
var react_fontawesome_1 = require("@fortawesome/react-fontawesome");
function default_1(_a) {
    var text = _a.text, icon = _a.icon, href = _a.href;
    return (React.createElement("li", null,
        React.createElement(core_1.ListItemButton, { component: react_router_dom_1.NavLink, to: href },
            React.createElement(core_1.ListItemIcon, null,
                React.createElement(react_fontawesome_1.FontAwesomeIcon, { icon: icon, fixedWidth: true })),
            React.createElement(core_1.ListItemText, { primary: text }))));
}
exports.default = default_1;
//# sourceMappingURL=DrawerItem.js.map