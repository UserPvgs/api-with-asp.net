using System.Net;
using Data.ApplicationDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Tokens;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Authorize]
public class AuthorizeController {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthorizeController(ApplicationDbContext context, IConfiguration configuration){
        _context = context;
        _configuration = configuration;
    }
    [HttpGet("info-user")]
    public IActionResult InfoUser([FromHeader(Name = "Authorization")] string Authorization){
        if(Authorization != null){
            var token = Authorization.Split(' ')[1];
            var tokenServiceApi = new TokenServiceApi();
            return new JsonResult(tokenServiceApi.getInfoTokenToUser(token, _configuration));
        }
        return new JsonResult("Error Authorization");
    }
}