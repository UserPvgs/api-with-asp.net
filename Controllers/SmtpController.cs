using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class SmtpController : ControllerBase {
    private readonly IConfiguration _configuration;

    public SmtpController(IConfiguration configuration){
        _configuration = configuration;
    }
    [HttpPost]
    public async Task<IActionResult> SendSmtp(){
        try{
            await new SmtpUseCase(_configuration).Build().Execute();
            return StatusCode(StatusCodes.Status200OK, new {message="E-mail enviado"});
        }catch(Exception e) when(e is DomainvalidationException || e is ArgumentException){
            return BadRequest(e.Message);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, new{message=e.Message});
        }
    }
}