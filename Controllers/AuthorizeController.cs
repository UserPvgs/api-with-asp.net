using System.ComponentModel.DataAnnotations;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Repositories;
using Services.Tokens;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Authorize]
public class AuthorizeController : ControllerBase {
    private readonly IUserRepository _repo;
    private readonly IConfiguration _configuration;
    public AuthorizeController(IUserRepository repo, IConfiguration configuration){
        _repo = repo;
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
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateUserForm user, [FromHeader(Name = "Authorization")] string Authorization){
        try{
            await new UpdateUserUseCase(_repo ,_configuration).Build(user, Authorization).Execute();
            return StatusCode(StatusCodes.Status200OK, new{message="user changed successfully."});
        }catch(Exception e) when (e is DomainvalidationException || e is ValidationException){
            return BadRequest(e.Message);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, new {error=e.Message});
        }
    }
}