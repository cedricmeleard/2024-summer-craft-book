using System.ComponentModel;

namespace PasswordValidation;

public class Password
{
    public string? Value { get; }
    private Password(string? passwordValue)
    {
        Value = passwordValue;
    }
    public static bool TryCreate(string? entry, out Password password)
    {
        bool result = PasswordValidator
            .Init(entry)
            .CheckPasswordMatchRequiredLength()
            .CheckPasswordHasAtLeastOneCapitalLetter()
            .CheckPasswordHasAtLeastOneLowercaseLetter()
            .CheckPasswordHasAtLeastOneDigit()
            .CheckPasswordHasAtLeastOneSpecialCharOf("*#@$%&.")
            .IsValid();
        
        // TODO something wrong here
        if (result) {
            password = new Password(entry);
            return true;
        }
        
        password = null;
        return false;
    }
}