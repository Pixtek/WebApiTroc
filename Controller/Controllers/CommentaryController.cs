using Infrastructure.EF.Commentary;
using Infrastructure.EF.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/Commentary")]
public class CommentaryController : ControllerBase
{
    private readonly ICommentary _ICommentary;

    public CommentaryController(ICommentary iCommentary)
    {
        _ICommentary = iCommentary;
    }
    
    [HttpGet]
    public IEnumerable<DbCommentary> GetAll()
    {
        return _ICommentary.GetAll();
    }
    
    [HttpPost]
    public ActionResult<DbCommentary> Create(short note,string nom, string mesasge, int id_User)
    {
        return Ok(_ICommentary.Create(note, nom, mesasge, id_User));
    }
    
    
    [HttpGet]
    [Route("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public ActionResult<DbCommentary> FetchByName(string name)
    {
        try
        {
            return _ICommentary.FetchByName(name);
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
    public ActionResult Update(DbCommentary dbCommentary)
    {
        return _ICommentary.Update(dbCommentary) ? NoContent() : NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        return _ICommentary.Delete(id) ? NoContent() : NotFound();
    }
    
    [HttpGet]
    [Route("Id_Users/{idUser:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IEnumerable<DbCommentary> FetchByIdUser(int idUser)
    {

        return _ICommentary.FetchByIdUser(idUser);

    }
    
    
}