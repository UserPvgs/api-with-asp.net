using System.Net.Mail;
using Services.Smtp;

public class SmtpUseCase{
    private readonly IConfiguration _configuration;
    private SmtpService _smtpService;
    public SmtpUseCase(IConfiguration configuration){
        _configuration = configuration;
    }
    public SmtpUseCase Build(){
        _smtpService = new SmtpService(new MailAddress(_configuration["Smtp:From"]), new MailAddress(_configuration["Smtp:To"]), _configuration);
        return this;
    }
    public async Task Execute(){
        await _smtpService.Build().Execute();
    }
}