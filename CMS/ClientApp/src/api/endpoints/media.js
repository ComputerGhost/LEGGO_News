"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.useUploadMedia = exports.useDeleteMedia = exports.useMedia = void 0;
var RestApi_1 = require("../RestApi");
var media = new RestApi_1.default('media');
function useMedia(search) {
    return media.useItems(search);
}
exports.useMedia = useMedia;
function useDeleteMedia(mediaId) {
    return media.useDeleteItem(mediaId);
}
exports.useDeleteMedia = useDeleteMedia;
function useUploadMedia() {
    return media.useUploadItem();
}
exports.useUploadMedia = useUploadMedia;
//# sourceMappingURL=media.js.map