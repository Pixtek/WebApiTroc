
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
    private readonly UseCaseCreateUser _useCaseCreateUser;
    private readonly UseCaseFetchByPseudo _useCaseFetchByPseudo;

    public UserController(IUsers iUsers, UseCaseFetchById useCaseFetchById, UseCaseCreateUser useCaseCreateUser, UseCaseFetchByPseudo useCaseFetchByPseudo)
    {
        _IUsers = iUsers;
        _useCaseFetchById = useCaseFetchById;
        _useCaseCreateUser = useCaseCreateUser;
        _useCaseFetchByPseudo = useCaseFetchByPseudo;
    }

    [HttpGet]
    public IEnumerable<DbUser> GetAll()
    {
        return _IUsers.GetAll();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputUser> Create(DtoInputCreateUser dto)
    {
        try
        {
            var output = _useCaseCreateUser.Execute(dto);
            return CreatedAtAction(
                nameof(FetchById),
                new { id = output.Id },
                output
            );
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
    
    [HttpGet]
    [Route("{pseudo}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchByPseudo(string pseudo)
    {
        try
        {
            return _useCaseFetchByPseudo.Execute(pseudo);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        return _IUsers.Delete(id) ? NoContent() : NotFound();
    }
    

}