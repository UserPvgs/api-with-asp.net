using Models.Repositories;
using Services.Cypher;
using Services.Tokens;

namespace Application.UseCases;
public class UpdateUserUseCase {
    private User ?_user;
    private string ?_authorization;
    private readonly IUserRepository _repo;
    private readonly IConfiguration _configuration;
    public UpdateUserUseCase(IUserRepository repo, IConfiguration configuration){
        _configuration = configuration;
        _repo = repo;
    }
    public UpdateUserUseCase Build(UpdateUserForm form, string authorization){
        _user = new User(form.Id, form.Name, form.Email, form.Password);
        _authorization = authorization;
        return this;
    }
    public async Task<User> Execute(){
        var tokenServiceApi = new TokenServiceApi();
        var token = _authorization?.Split(' ')[1];
        var tokenInfoUser = tokenServiceApi.getInfoTokenToUser(token, _configuration);
        if(tokenInfoUser == null){
            throw new ArgumentException("Invalid User");
        }
        var courseToUpdate = await _repo.UserByEmailAsync(tokenInfoUser);
        if(courseToUpdate != null){
            var cryptoServiceApi = new CryptoServiceApi();
            _user.Password = cryptoServiceApi.Encrypt(_user.Password);
            courseToUpdate.Email = _user.Email;
            courseToUpdate.Password = _user.Password;
            courseToUpdate.Name = _user.Name;
            var updateUser = await _repo.UpdateUser(courseToUpdate);
            return updateUser;
        }
        throw new ArgumentException("User not att");
    }
}