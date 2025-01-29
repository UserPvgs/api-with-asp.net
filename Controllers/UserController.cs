using Data.ApplicationDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Cypher;
using Services.Tokens;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
public class UserController : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public UserController(IConfiguration configuration, ApplicationDbContext context){
    _configuration = configuration;
    _context = context;
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
    public async Task<IActionResult> Create([FromBody] User user){
        if(user == null){
            return BadRequest("Invalid request");
        }
        if(string.IsNullOrEmpty(user.Email)){
            return BadRequest("Email not informed");
        }
        if(string.IsNullOrEmpty(user.Password)){
            return BadRequest("Password not informed");
        }
        if(string.IsNullOrEmpty(user.Name)){
            return BadRequest("Name not informed");
        }
        var cryptoServiceApi = new CryptoServiceApi();
        user.Password = cryptoServiceApi.Encrypt(user.Password);
        _context.Add(user);
        var userCreated = await _context.SaveChangesAsync();
        if(userCreated > 0){
            return new JsonResult(true);
        }
        return BadRequest("User not created");
    } 
}