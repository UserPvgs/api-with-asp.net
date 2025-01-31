namespace LearningCSharp.Models.Validations; 

public class DomainValidation {
    public static void When(bool validationFail, string message) {
        if (validationFail) throw new DomainValidationException(message);
    }
}