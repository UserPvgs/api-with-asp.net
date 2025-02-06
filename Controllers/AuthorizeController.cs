using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Cypher;
using Services.Tokens;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Authorize]
public class AuthorizeController : ControllerBase{
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
    [HttpPut("update-user")]
    public async Task<IActionResult> Update<BaseModel>([FromBody] User user, [FromHeader(Name = "Authorization")] string Authorization) where BaseModel : User{
        var tokenServiceApi = new TokenServiceApi();
        var token = Authorization.Split(' ')[1];
        var tokenInfoUser = tokenServiceApi.getInfoTokenToUser(token, _configuration);
        if(tokenInfoUser == null){
            return new JsonResult("Invalid token");
        }
        var courseToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Email == tokenInfoUser);
        if(courseToUpdate != null){
            var cryptoServiceApi = new CryptoServiceApi();
            user.Password = cryptoServiceApi.Encrypt(user.Password);
            courseToUpdate.Email = user.Email;
            courseToUpdate.Password = user.Password;
            courseToUpdate.Name = user.Name;
            _context.Users.Update(courseToUpdate);
            await _context.SaveChangesAsync();
            return new JsonResult(true);
        }
        return new JsonResult("User not att");
    }
}