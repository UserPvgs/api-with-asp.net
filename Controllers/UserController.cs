using Data.ApplicationDbContext;
using LearningCSharp.Application.Forms;
using LearningCSharp.Application.UseCases;
using LearningCSharp.Models.Repositories;
using LearningCSharp.Models.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Cypher;
using Services.Tokens;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
public class UserController : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repo;

    public UserController(IConfiguration configuration, IUserRepository repo){
    _configuration = configuration;
    _repo = repo;
    }
    
    [HttpPost("login-user")]
    public async Task<IActionResult> LoginUser([FromBody] User user)
    {
        if(user == null){
            return BadRequest("Invalid Request");
        }
        if(string.IsNullOrEmpty(user.Email))
        {
            return BadRequest("Email not informed");
        }
        if(string.IsNullOrEmpty(user.Password))
        {
            return BadRequest("Password not informed");
        }
        var cryptoServiceApi = new CryptoServiceApi();
        var userResponse = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if(userResponse != null && cryptoServiceApi.CompareHash(user.Password, userResponse.Password) == true){
            var TokenServiceApi = new TokenServiceApi();
            return new JsonResult(new {token = TokenServiceApi.GenerateToken(user.Email, _configuration), userFound = userResponse});
        }
        return new JsonResult("Invalid user");
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateNewUserForm user) 
    {
        try 
        {
            await new CreateUserUseCase(_repo).Build(user).Execute();
            return StatusCode(StatusCodes.Status201Created, new { message = "Usuario cadastrado com sucesso!" });
        }
        catch (Exception e) when (e is DomainValidationException || e is ArgumentException) 
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}