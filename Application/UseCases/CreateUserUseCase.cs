using Models.Repositories;
using Services.Cypher;
public class CreateUserUseCase {
    private User _newUser;
    private readonly IUserRepository _repo;

    public CreateUserUseCase(IUserRepository repo){
        _repo = repo;
    }
    public CreateUserUseCase Build(CreateNewUserForm form){
        _newUser = new User(form.Email, form.Password, form.Name);
        return this;
    }
    public async Task Execute(){
        var issetUser = await _repo.UserByEmailAsync(_newUser.Email);
        if(issetUser is not null) throw new ArgumentException("User already exists");
        var cryptoServiceApi = new CryptoServiceApi();
        _newUser.Password = cryptoServiceApi.Encrypt(_newUser.Password);
        await _repo.CreateUserAsync(_newUser);
    }
}