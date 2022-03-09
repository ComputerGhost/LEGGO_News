"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var default_1 = /** @class */ (function (_super) {
    __extends(default_1, _super);
    function default_1(status) {
        var _this = _super.call(this, "API returned error code {status}.") || this;
        _this.name = "ApiError";
        _this.status = status;
        return _this;
    }
    return default_1;
}(Error));
exports.default = default_1;
//# sourceMappingURL=ApiError.js.map