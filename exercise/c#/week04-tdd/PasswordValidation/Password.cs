using System.ComponentModel;

namespace PasswordValidation;

public class Password
{
    public string? Value { get; }
    private Password(string? passwordValue)
    {
        Value = passwordValue;
    }
    public static bool TryCreate(string? entry, out Password? password)
    {
        password = null;
        
        bool result = PasswordValidator
            .Init(entry)
            .CheckPasswordMatchRequiredLength()
            .CheckPasswordHasAtLeastOneCapitalLetter()
            .CheckPasswordHasAtLeastOneLowercaseLetter()
            .CheckPasswordHasAtLeastOneDigit()
            .CheckPasswordHasAtLeastOneSpecialCharOf("*#@$%&.")
            .CheckPasswordMeetRequiredChars("^[a-zA-Z0-9.*#@$%&]+$")
            .IsValid();

        if (!result) {
            return false;
        }
        
        password = new Password(entry);
        return true;

    }
}