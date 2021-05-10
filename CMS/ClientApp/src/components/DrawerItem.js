"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var core_1 = require("@material-ui/core");
var reactstrap_1 = require("reactstrap");
var react_fontawesome_1 = require("@fortawesome/react-fontawesome");
function default_1(_a) {
    var text = _a.text, icon = _a.icon, to = _a.to;
    return (React.createElement(core_1.ListItem, { component: reactstrap_1.NavLink, to: to },
        React.createElement(core_1.ListItemIcon, null,
            React.createElement(react_fontawesome_1.FontAwesomeIcon, { icon: icon, fixedWidth: true })),
        React.createElement(core_1.ListItemText, { primary: text })));
}
exports.default = default_1;
//# sourceMappingURL=DrawerItem.js.map