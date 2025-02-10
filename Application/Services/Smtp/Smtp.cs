using System.Net;
using System.Net.Mail;
using System.Text;
namespace Services.Smtp;
public class SmtpService{
    private MailAddress _from;
    private MailAddress _to;
    private MailMessage _message;
    private IConfiguration _configuration;
    public SmtpService(MailAddress from, MailAddress to, IConfiguration configuration){
        _from = from;
        _to = to;
        _configuration = configuration;
    } 
    public SmtpService Build(){
        _message = new MailMessage(_from, _to);
        return this;
    }
    public async Task Execute(){
        _message.Subject = "teste";
        _message.IsBodyHtml = true;
        _message.Body = "<p>Welcome to application</p>";
        _message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
        _message.BodyEncoding = Encoding.GetEncoding("UTF-8");
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(_configuration["Smtp:From"], _configuration["Smtp:AppPassword"]);
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(_message);
    }
}