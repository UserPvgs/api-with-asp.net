using System.Net.Mail;
using System.Text.Json;
using Models.Repositories;
using Services.Smtp;

public class SmtpUseCase{
    private readonly IConfiguration _configuration;
    private QueueTask _queueTask;
    private IQueueTaskRepository _repo;
    public SmtpUseCase(IQueueTaskRepository repo){
        _repo = repo;
    }
    public SmtpUseCase Build(CreateNewQueueForm queueForm){
        var payload = new{subject=queueForm.Subject, isBodyHtml=true, body=queueForm.Body, to=queueForm.To};
        string jsonPayload = JsonSerializer.Serialize(payload);
        _queueTask = new QueueTask(queueForm.Status, queueForm.TaskType, jsonPayload);
        return this;
    }
    public async Task<bool> Execute(){
        await _repo.CreateQueueAsync(_queueTask);
        return true;
    }
}