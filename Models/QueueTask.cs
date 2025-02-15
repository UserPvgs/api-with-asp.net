public class QueueTask {
    public int Id {get; set;}
    public string Status {get; set;}
    public string TaskType {get; set;}
    public string PayLoad {get; set;}

    public QueueTask(){ }

    public QueueTask(string status, string tasktype, string payload){
        DomainValidation.When(string.IsNullOrEmpty(status), "Status invalid");
        DomainValidation.When(string.IsNullOrEmpty(tasktype), "Type of task is invalid");
        DomainValidation.When(string.IsNullOrEmpty(payload), "Payload is invalid");
        Status = status;
        TaskType = tasktype;
        PayLoad = payload;
    }
    public QueueTask(int id, string status, string tasktype, string payload){
        DomainValidation.When(string.IsNullOrEmpty(status), "Status invalid");
        DomainValidation.When(string.IsNullOrEmpty(tasktype), "Type of task is invalid");
        DomainValidation.When(string.IsNullOrEmpty(payload), "Payload is invalid");
        Id = id;
        Status = status;
        TaskType = tasktype;
        PayLoad = payload;
    }
}