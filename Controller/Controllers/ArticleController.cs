using Domain;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticle _IArticle;

    public ArticleController(IArticle iArticle)
    {
        _IArticle = iArticle;
    }

    [HttpGet]
    public IEnumerable<Article> GetAll()
    {
        return _IArticle.GetAll();
    }
}