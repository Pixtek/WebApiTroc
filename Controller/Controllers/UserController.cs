
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Application.UseCases.Users;
using Application.UseCases.Users.Dto;
using Domain;
using Infrastructure.EF.DbEntities;
using Infrastructure.EF.JwtAuthentication;
using Infrastructure.EF.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Internal;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/Users")]
public class UserController :ControllerBase
{

    private readonly IUsers _IUsers;
    private readonly UseCaseFetchById _useCaseFetchById;
    private readonly UseCaseCreateUser _useCaseCreateUser;
    private readonly UseCaseFetchByPseudo _useCaseFetchByPseudo;
    private readonly IJwtAuthenticationService _jwtAuthenticationService;
    private readonly IConfiguration _config;


    public UserController(IUsers iUsers, UseCaseFetchById useCaseFetchById, UseCaseCreateUser useCaseCreateUser, UseCaseFetchByPseudo useCaseFetchByPseudo, IJwtAuthenticationService jwtAuthenticationService, IConfiguration config)
    {
        _IUsers = iUsers;
        _useCaseFetchById = useCaseFetchById;
        _useCaseCreateUser = useCaseCreateUser;
        _useCaseFetchByPseudo = useCaseFetchByPseudo;
        _jwtAuthenticationService = jwtAuthenticationService;
        _config = config;
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
        catch (KeyNotFoundException e )
        {
            return NotFound(new
            {
                e.Message
            });
        }
        catch (SyntaxErrorException e )
        {
            return Unauthorized(new
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
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var user = _jwtAuthenticationService.Authenticate(model.Email, model.Mdp);
        
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email",user.Email),
                new Claim("pseudo", user.Pseudo),
                new Claim("localite", user.Localite)
            };
            var token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);
            Response.Cookies.Append("AUTH_COOKIE", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true
            });
            return Ok(token);
        }
        return Unauthorized();
    }
    
    
    [HttpGet]
    [Route("fetchById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchById()
    {
        try
        {
            var test = User.Claims;
            var id = User.Claims.First(claim => claim.Type == "id").Value;
            
            return  _useCaseFetchById.Execute(Convert.ToInt32(id));
        }
        catch (Exception e)
        {
            
            return NotFound("dfdd");
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
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateUser(String email, String pseudo, String localite, int id)
    {
        return _IUsers.Update(email, pseudo,localite,id) ? NoContent() : NotFound();
    }  
    
    [HttpPut]
    [Route("setAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult SetAdmin(bool admin, int id)
    {
        return _IUsers.SetAdmin(admin,id) ? NoContent() : NotFound();
    }
    
    [HttpPost]
    [Route("disconnect")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Disconnect()
    {
        Response.Cookies.Delete("AUTH_COOKIE");
        return Ok();
    }
    
    [HttpPost]
    [Route("connexion")]
    public ActionResult<Users> Connexion(String email, String mdp)
    {
        var user = _jwtAuthenticationService.Authenticate(email, mdp);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email",user.Email),
                new Claim("pseudo", user.Pseudo),
                new Claim("localite", user.Localite)
            };
            var token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);
            Response.Cookies.Append("AUTH_COOKIE", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true
            });

            Response.Cookies.Append("cookieNonSecurise", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = false
            });
            return Ok(user);
        }
        return Unauthorized(null);
    }

    

}