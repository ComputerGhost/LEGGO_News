"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.useDeleteArticle = exports.useUpdateArticle = exports.useCreateArticle = exports.useArticle = exports.useArticles = void 0;
var RestApi_1 = require("../RestApi");
var articles = new RestApi_1.default('articles');
function useArticles(search) {
    return articles.useItems(search);
}
exports.useArticles = useArticles;
function useArticle(articleId) {
    return articles.useItem(articleId);
}
exports.useArticle = useArticle;
function useCreateArticle() {
    return articles.useCreateItem();
}
exports.useCreateArticle = useCreateArticle;
function useUpdateArticle(articleId) {
    if (!articleId) {
        throw new Error('articleId must be defined to update.');
    }
    return articles.useUpdateItem(articleId);
}
exports.useUpdateArticle = useUpdateArticle;
function useDeleteArticle(articleId) {
    return articles.useDeleteItem(articleId);
}
exports.useDeleteArticle = useDeleteArticle;
//# sourceMappingURL=articles.js.map