
using Application.UseCases.Users;
using Application.UseCases.Users.Dto;
using Infrastructure.EF.DbEntities;
using Infrastructure.EF.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/Users")]
public class UserController :ControllerBase
{
    private readonly IUsers _IUsers;
    private readonly UseCaseFetchById _useCaseFetchById;
    

    public UserController(IUsers iUsers, UseCaseFetchById useCaseFetchById)
    {
        _IUsers = iUsers;
        _useCaseFetchById = useCaseFetchById;
    }

    [HttpGet]
    public IEnumerable<DbUser> GetAll()
    {
        return _IUsers.GetAll();
    }

    [HttpPost]
    public ActionResult<DbUser> Create(string email, string pseudo, string localite, string mdp)
    {
        try
        {
            return Ok(_IUsers.Create( email,  pseudo,  localite,  mdp));
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
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchById(int id)
    {
        try
        {
            return _useCaseFetchById.Execute(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    

}