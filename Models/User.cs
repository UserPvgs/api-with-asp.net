using System.ComponentModel.DataAnnotations;
using System.Numerics;
using LearningCSharp.Models.Validations;
using Microsoft.EntityFrameworkCore;

public class User{
    public int Id {get; set;}
    public string Email{get; set;}
    public string Password {get; set;}
    public string Name{get; set;}

    public User(string email, string password, string name = "") {
        DomainValidation.When(string.IsNullOrEmpty(email), "O campo de email é obrigatório");
        DomainValidation.When(string.IsNullOrEmpty(password), "O campo senha é obrigatório");
        
        Email = email;
        Password = password;
        Name = name;
    }

    public User(int id, string email, string password, string name) {
        DomainValidation.When(id <= 0, "O id de usuario é inválido");
        DomainValidation.When(string.IsNullOrEmpty(email), "O campo de email é obrigatório");
        DomainValidation.When(string.IsNullOrEmpty(password), "O campo senha é obrigatório");

        Id = id;
        Email = email;
        Password = password;
        Name = name;
    }
}