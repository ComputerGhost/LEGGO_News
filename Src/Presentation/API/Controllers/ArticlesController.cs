using Core.Modules.ArticleList.Models;
using Core.Modules.ArticleList.Queries.GetMostRecent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly ISender _sender;

    public ArticlesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedArticles))]
    public Task<PagedArticles> Index()
    {
        var query = new GetMostRecentArticlesQuery();
        return _sender.Send(query);
    }
}
