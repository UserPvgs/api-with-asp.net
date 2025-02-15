using Data;
using Microsoft.EntityFrameworkCore;
using Models.Repositories;

public class QueueRepository : IQueueTaskRepository {
    private readonly ApplicationDbContext _context;
    public QueueRepository(ApplicationDbContext context){
        _context = context;
    }
    public async Task<QueueTask> QueueTaskAsync(){
        var QueueTask = await _context.QueueTasks.FirstOrDefaultAsync();
        return QueueTask;
    }
    public async Task<List<QueueTask>> VerifyToDoTasksAsync(){
        return await _context.QueueTasks.Where(q => q.Status.Equals("Pending")).ToListAsync();
    }
    public async Task UpdateQueueAsync(QueueTask queue){
        _context.QueueTasks.Update(queue);
        await _context.SaveChangesAsync();
    }
}