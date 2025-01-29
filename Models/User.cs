using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

public class User{
    [Key]
    public int Id{get; private set;}
    [EmailAddress]
    public string Email{get; set;}
    public string Password {get; set;}
    public string ?Name{get; set;}
}