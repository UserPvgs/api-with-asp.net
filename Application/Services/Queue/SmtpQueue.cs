using System.Net.Mail;
using System.Text.Json;
using Models.Repositories;
using Services.Smtp;

public class EmailInfo {
    public required string subject{get; set;}
    public bool isBodyHtml{get; set;}
    public required string body{get; set;}
    public required string to{get; set;}   
}

public class SmtpQueuBackgroundService : BackgroundService {
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    public SmtpQueuBackgroundService(IConfiguration configuration, IServiceProvider serviceProvider){
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken){
        while(!cancellationToken.IsCancellationRequested){
            await ProcessQueueAsync();
            await Task.Delay(5000, cancellationToken);
        }
    }
    private async Task ProcessQueueAsync(){
        using(var scope = _serviceProvider.CreateScope()){
            var _repo = scope.ServiceProvider.GetRequiredService<IQueueTaskRepository>();
            try{
                List<QueueTask> queue = await _repo.VerifyToDoTasksAsync();
                if(queue is null){
                    Console.WriteLine("not queue found");
                    return;
                }
                foreach(QueueTask task in queue){
                    var infoSmtp = JsonSerializer.Deserialize<EmailInfo>(task.PayLoad);
                    await new SmtpService(new MailAddress("testesmtpservice@gmail.com"), new MailAddress(infoSmtp.to), _configuration).Build().Execute();
                    task.Status = "Completed";
                    await _repo.UpdateQueueAsync(task);
                    Console.WriteLine("E-mail enviado e status atualizado");
                    return;
                }
            }catch(Exception e) when(e is DomainvalidationException || e is ArgumentException){
                Console.WriteLine(e.Message);
                return;
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}