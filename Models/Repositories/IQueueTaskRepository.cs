namespace Models.Repositories;
public interface IQueueTaskRepository {
    Task<QueueTask> QueueTaskAsync();
    Task<List<QueueTask>> VerifyToDoTasksAsync();
    Task UpdateQueueAsync(QueueTask queue);

    Task CreateQueueAsync(QueueTask queue);
}