namespace LearningCSharp.Models.Repositories;

public interface IUserRepository {
    Task<User> AddAsync(User newUser);
    Task<User?> GetUseByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
}