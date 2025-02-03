namespace LearningCSharp.Models.Repositories;

public interface IUserRepository {
    Task<User> AddAsync(User newUser);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
}