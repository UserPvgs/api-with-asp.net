using Data.ApplicationDbContext;
using LearningCSharp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository {
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<User> AddAsync(User newUser) {
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> GetUserByIdAsync(int userId) {
        var user = await _context.Users.FindAsync(userId);
        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email) {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }
}