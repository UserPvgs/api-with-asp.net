//O dominio(model) deve ter a regra de negocio
public class User{
    public int Id{get; set;}
    public string Email{get; set;}
    public string Password {get; set;}
    public string ?Name{get; set;}
    public User(string ?name, string email, string password){
        //Adicionar verificações com o Domain validation
        //DomainValidation.When(string.IsNullOrEmpty(name), "O nome é obrigatório");
        DomainValidation.When(string.IsNullOrEmpty(email), "O campo email é obrigatório");
        DomainValidation.When(string.IsNullOrEmpty(password), "O campo Password é obrigatório");
        Name = name;
        Email = email;
        Password = password;
    }
    public User(int id, string name, string email, string password) {
        DomainValidation.When(string.IsNullOrEmpty(email), "O campo email é obrigatório");
        DomainValidation.When(string.IsNullOrEmpty(password), "O campo Password é obrigatório");
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}