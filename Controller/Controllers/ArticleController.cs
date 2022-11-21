using Domain;
using Infrastructure.EF;
using Infrastructure.EF.Article;
using Infrastructure.EF.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/Article")]
public class ArticleController : ControllerBase
{
    private readonly IArticle _IArticle;

    public ArticleController(IArticle iArticle)
    {
        _IArticle = iArticle;
    }

    [HttpGet]
    public IEnumerable<DbArticle> GetAll()
    {
        return _IArticle.GetAll();
    }

    [HttpPost]
    public ActionResult<Article> Create(int idUser, string name, string urlImage,
        DateTime publicationDate,string nomCat)
    {
        return Ok(_IArticle.Create(idUser, name, urlImage, publicationDate,nomCat));
    }
}