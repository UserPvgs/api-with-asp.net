namespace Models.Repositories;
public interface IUserRepository {
    Task<User> CreateUserAsync(User user);
    Task<User?> UserByEmailAsync(string email); 
    Task<User> UpdateUser(User user);
}