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
    public ActionResult<DbCommentary> Create(int note,string nom, string mesasge, int id_User)
    {
        return Ok(_ICommentary.Create(note, nom, mesasge, id_User));
    }
}