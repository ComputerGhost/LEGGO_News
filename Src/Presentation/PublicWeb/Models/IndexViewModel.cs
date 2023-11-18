using Core.Modules.ArticleList.Models;
using Core.Modules.ArticleList.Queries.GetMostRecent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PublicWeb.Models;

public class IndexViewModel
{
    [FromQuery]
    public string? Search { get; set; }

    public PagedArticles Articles { get; set; }

    public async Task Update(ISender sender)
    {
        if (string.IsNullOrEmpty(Search))
        {
            var query = new GetMostRecentArticlesQuery();
            Articles = await sender.Send(query);
        }
        else
        {
            // search
        }
    }
}
