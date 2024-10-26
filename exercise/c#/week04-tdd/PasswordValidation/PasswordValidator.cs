using System.Text.RegularExpressions;

namespace PasswordValidation;

public class PasswordValidator
{
    private readonly bool _isValid;
    private readonly string _entry;
    public bool IsValid() => _isValid;

    private PasswordValidator(string entry, bool isValid)
    {
        _entry = entry;
        _isValid = isValid;
    }
    public static PasswordValidator Init(string? entry) 
        => string.IsNullOrEmpty(entry) ? Failed : new(entry, true);

    private static PasswordValidator Failed => new("", false);
    private PasswordValidator Success() => new(_entry!,true);
    
    public PasswordValidator CheckPasswordMatchRequiredLength()
    {
        // Considering escaped sequences as a invalid password
        bool noEscapedChars = Regex.Matches(_entry, @"\\[0-9]{3}").Count == 0;
        
        return noEscapedChars && _entry.Length >= 8 
            ? Success() 
            : Failed;
    }
    public PasswordValidator CheckPasswordHasAtLeastOneCapitalLetter() 
        => _isValid && _entry.Any(char.IsUpper) ? Success() : Failed;
    public PasswordValidator CheckPasswordHasAtLeastOneLowercaseLetter() 
        => _isValid && _entry.Any(char.IsLower) ? Success() : Failed;
    public PasswordValidator CheckPasswordHasAtLeastOneDigit()
        => _isValid && _entry.Any(char.IsDigit) ? Success() : Failed;
}

