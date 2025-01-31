using LearningCSharp.Application.Forms;
using LearningCSharp.Models.Repositories;

namespace LearningCSharp.Application.UseCases; 

public class CreateUserUseCase {
    private User _newUser;
    private IUserRepository _repo;

    public CreateUserUseCase(IUserRepository repo) {
        _repo = repo;
    }

    public CreateUserUseCase Build(CreateNewUserForm form) {
        _newUser = new User(form.Email, form.Password, form.Name);
        return this;
    }

    public async Task Execute() {
        User? alreadyRegisterdUser = await _repo.GetUserByEmailAsync(_newUser.Email);
        if (alreadyRegisterdUser is not null) throw new ArgumentException("ja é existe usuario cadastrado para o email fornecido");
        await _repo.AddAsync(_newUser);
    }
}