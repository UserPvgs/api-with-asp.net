using Application.Forms;
using Data.Repositories;
using Models.Repositories;
using Services.Cypher;
using Services.Tokens;

namespace Application.UseCases;
public class LoginUserUseCase {
    private User _user;
    private readonly IUserRepository _repo;
    private readonly IConfiguration _configuration;
    public LoginUserUseCase(IUserRepository repo, IConfiguration configuration){
        _repo = repo;
        _configuration = configuration;
    }
    public LoginUserUseCase Build(LoginUserForm form){
        _user = new User("", form.Email, form.Password);
        return this;
    }
    public async Task<string> Execute(){
        var user = await _repo.UserByEmailAsync(_user.Email);
        if(user is null) throw new ArgumentException("User not found");
        var cryptoServiceApi = new CryptoServiceApi();
        if(cryptoServiceApi.CompareHash(_user.Password, user.Password) == true){
            var tokenServiceApi = new TokenServiceApi();
            return tokenServiceApi.GenerateToken(user.Email, _configuration);
        }
        throw new ArgumentException("Invalid User");
    }
}