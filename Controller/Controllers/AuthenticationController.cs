using System.Security.Claims;
using Domain;
using Infrastructure.EF.JwtAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace WebApiTroc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IJwtAuthenticationService _jwtAuthenticationService;
    private readonly IConfiguration _config;


    public AuthenticationController(IJwtAuthenticationService jwtAuthenticationService, IConfiguration config)
    {
        _jwtAuthenticationService = jwtAuthenticationService;
        _config = config;
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
            
            Response.Cookies.Append("cookie", token, new CookieOptions()
            {
                HttpOnly = false,
                Secure = false
            });

            return Ok();
        }

        return Unauthorized();
    }

}