"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var styles_1 = require("@material-ui/core/styles");
var core_1 = require("@material-ui/core");
var useStyles = function (leftMargin) { return styles_1.makeStyles(function (theme) { return ({
    bar: {
        paddingLeft: leftMargin,
    },
    title: {
        flexGrow: 1,
    }
}); })(); };
function TopBar(_a) {
    var leftMargin = _a.leftMargin;
    var classes = useStyles(leftMargin);
    return (React.createElement(core_1.AppBar, { position: 'static', className: classes.bar },
        React.createElement(core_1.Toolbar, null,
            React.createElement(core_1.Typography, { variant: 'h6', className: classes.title }, "LEGGO News"),
            React.createElement(core_1.Button, { color: 'inherit' }, "Login"))));
}
exports.default = TopBar;
//# sourceMappingURL=TopBar.js.map