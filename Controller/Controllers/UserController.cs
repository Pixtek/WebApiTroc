using Domain;
using Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class UserController :ControllerBase
{
    private readonly IUsers _IUsers;
    

    public UserController(IUsers iUsers)
    {
        _IUsers = iUsers;
    }

    [HttpGet]
    public IEnumerable<Users> GetAll()
    {
        return _IUsers.GetAll();
    }

    [HttpPost]
    public ActionResult<Users> Create(string email, string pseudo, string localite, string mdp)
    {
        return Ok(_IUsers.Create( email,  pseudo,  localite,  mdp));
    }
}