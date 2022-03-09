"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.useDeleteTag = exports.useUpdateTag = exports.useCreateTag = exports.useTag = exports.useTags = void 0;
var RestApi_1 = require("../RestApi");
var tags = new RestApi_1.default('tags');
function useTags(search) {
    return tags.useItems(search);
}
exports.useTags = useTags;
function useTag(tagId) {
    return tags.useItem(tagId);
}
exports.useTag = useTag;
function useCreateTag() {
    return tags.useCreateItem();
}
exports.useCreateTag = useCreateTag;
function useUpdateTag(tagId) {
    if (!tagId)
        throw new Error('tagId must be defined to update.');
    return tags.useUpdateItem(tagId);
}
exports.useUpdateTag = useUpdateTag;
function useDeleteTag(tagId) {
    return tags.useDeleteItem(tagId);
}
exports.useDeleteTag = useDeleteTag;
//# sourceMappingURL=tags.js.map