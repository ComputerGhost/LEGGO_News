"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var styles_1 = require("@material-ui/core/styles");
var core_1 = require("@material-ui/core");
var free_solid_svg_icons_1 = require("@fortawesome/free-solid-svg-icons");
var DrawerItem_1 = require("./DrawerItem");
var useStyles = function (width) { return styles_1.makeStyles(function (theme) { return ({
    drawer: {
        width: width,
    },
    header: {
        fontSize: '1rem',
        paddingLeft: theme.spacing(2),
    },
    toolbar: theme.mixins.toolbar
}); })(); };
function NavDrawer(_a) {
    var width = _a.width;
    var classes = useStyles(width);
    return (React.createElement(core_1.Drawer, { classes: { paper: classes.drawer }, open: true, variant: 'persistent' },
        React.createElement("div", { className: classes.toolbar }),
        React.createElement(core_1.List, null,
            React.createElement(DrawerItem_1.default, { text: 'Dashboard', icon: free_solid_svg_icons_1.faHome, to: '/' })),
        React.createElement(core_1.Typography, { variant: 'h6', className: classes.header }, "Content"),
        React.createElement(core_1.List, null,
            React.createElement(DrawerItem_1.default, { text: 'Articles', icon: free_solid_svg_icons_1.faPenNib, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Leads', icon: free_solid_svg_icons_1.faLightbulb, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Comments', icon: free_solid_svg_icons_1.faComments, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Media', icon: free_solid_svg_icons_1.faPhotoVideo, to: '/' })),
        React.createElement(core_1.Typography, { variant: 'h6', className: classes.header }, "Setup"),
        React.createElement(core_1.List, null,
            React.createElement(DrawerItem_1.default, { text: 'Tags', icon: free_solid_svg_icons_1.faTags, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Characters', icon: free_solid_svg_icons_1.faTheaterMasks, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Templates', icon: free_solid_svg_icons_1.faShapes, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Users', icon: free_solid_svg_icons_1.faUsers, to: '/' })),
        React.createElement(core_1.Typography, { variant: 'h6', className: classes.header }, "Information"),
        React.createElement(core_1.List, null,
            React.createElement(DrawerItem_1.default, { text: 'Licenses', icon: free_solid_svg_icons_1.faHandshake, to: '/' }),
            React.createElement(DrawerItem_1.default, { text: 'Help and Tips', icon: free_solid_svg_icons_1.faQuestionCircle, to: '/' }))));
}
exports.default = NavDrawer;
//# sourceMappingURL=NavDrawer.js.map