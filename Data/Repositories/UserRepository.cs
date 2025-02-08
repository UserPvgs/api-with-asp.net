using Data;
using Microsoft.EntityFrameworkCore;
using Models.Repositories;
namespace Data.Repositories;
public class UserRepository: IUserRepository{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context){
        _context = context;
    }
    public async Task<User> CreateUserAsync(User user){
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User?> UserByEmailAsync(string email){
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }
    public async Task<User> UpdateUser(User user){
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}