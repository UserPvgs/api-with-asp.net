using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Repositories;
[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
public class SmtpController : ControllerBase {
    private readonly IQueueTaskRepository _repo;

    public SmtpController(IQueueTaskRepository repo){
        _repo = repo;
    }
    [HttpPost]
    public async Task<IActionResult> SendSmtp(CreateNewQueueForm queueForm){
        try{
            await new SmtpUseCase(_repo).Build(queueForm).Execute();
            return StatusCode(StatusCodes.Status200OK, new {message="E-mail enviado"});
        }catch(Exception e) when(e is DomainvalidationException || e is ArgumentException){
            return BadRequest(e.Message);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, new{message=e.Message});
        }
    }
}