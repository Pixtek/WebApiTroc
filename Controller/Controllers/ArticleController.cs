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

    [HttpGet]
    [Route("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public ActionResult<DbArticle> FetchByName(string name)
    {
        try
        {
            return _IArticle.FetchByName(name);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(DbArticle dbArticle)
    {
        return _IArticle.Update(dbArticle) ? NoContent() : NotFound();
    }


}