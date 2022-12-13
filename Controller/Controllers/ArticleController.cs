using System.Security.Cryptography;
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
        DateTime publicationDate, string nomCat, string description)
    {
        return Ok(_IArticle.Create(idUser, name, urlImage, publicationDate, nomCat, description));
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<DbArticle> FetchByName([FromQuery]string name)
    {
        return _IArticle.FetchByName(name);
    }
    
    [HttpGet]
    [Route("{categoryName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IEnumerable<DbArticle> FetchByCategory(string categoryName)
    {
        return _IArticle.FetchByCategory(categoryName);
    }
    
    [HttpGet]
    [Route("Id/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DbArticle> FetchById(int id)
    {
        try
        {
            return _IArticle.FetchById(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    [HttpGet]
    [Route("Id_Users/{id_users:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IEnumerable<DbArticle> FetchById_Users(int id_users)
    {

        return _IArticle.FetchById_Users(id_users);

    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(DbArticle dbArticle)
    {
        return _IArticle.Update(dbArticle) ? NoContent() : NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        return _IArticle.Delete(id) ? NoContent() : NotFound();
    }


}