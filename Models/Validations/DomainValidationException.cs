namespace LearningCSharp.Models.Validations; 

public class DomainValidationException : Exception {
    public DomainValidationException(string message) : base(message) { }
}