public class DomainValidation{
    public static void When(bool validationFail, string message){
        if(validationFail) throw new DomainvalidationException(message);
    }
}