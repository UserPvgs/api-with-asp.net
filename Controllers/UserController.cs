using Application.Forms;
using Application.UseCases;
using Data;
using Microsoft.AspNetCore.Mvc;
using Models.Repositories;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
public class UserController : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repo;
    private readonly ApplicationDbContext _context;

    public UserController(IConfiguration configuration, IUserRepository repo, ApplicationDbContext context){
    _configuration = configuration;
    _repo = repo;
    _context = context;
    }
    [HttpGet]
    public bool test(){
        return true;
    }
    [HttpPost("login-user")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserForm user)
    {
        try{
            var responseToken = await new LoginUserUseCase(_repo, _configuration).Build(user).Execute();
            return StatusCode(StatusCodes.Status200OK, new {token=responseToken});
        }catch(Exception e) when(e is DomainvalidationException || e is ArgumentException){
            return BadRequest(e.Message);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, new {message=e.Message});
        }
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateNewUserForm user){
        try{
            await new CreateUserUseCase(_repo).Build(user).Execute();
            return StatusCode(StatusCodes.Status201Created, new {message="User created"});
        }catch(Exception e) when(e is DomainvalidationException || e is ArgumentException){
            return BadRequest(e.Message);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, new {message= e.Message});
        }
    }
}