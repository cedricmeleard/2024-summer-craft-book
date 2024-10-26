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
    
    private static PasswordValidator Failed => new PasswordValidatorError();
    private PasswordValidator Success() => new(_entry!,true);
    
    public virtual PasswordValidator CheckPasswordMatchRequiredLength()
    {
        int escapedSequenceCount = Regex.Matches(_entry, @"\\[0-9]{3}").Count;
        // Calculate real length considering escaped sequences as one character
        bool isRealLengthGreaterOrEqualToEight = _entry.Length - escapedSequenceCount * 3 + escapedSequenceCount >= 8;

        bool hasEscapedChars = escapedSequenceCount != 0;
        
        return !hasEscapedChars && isRealLengthGreaterOrEqualToEight  
            ? Success() 
            : Failed;
    }
    public virtual PasswordValidator CheckPasswordHasAtLeastOneCapitalLetter() 
        => _entry.Any(char.IsUpper) ? Success() : Failed;
    public virtual PasswordValidator CheckPasswordHasAtLeastOneLowercaseLetter() 
        => _entry.Any(char.IsLower) ? Success() : Failed;

    private sealed class PasswordValidatorError() : PasswordValidator("", false)
    {
        public override PasswordValidator CheckPasswordMatchRequiredLength() => Failed;
        public override PasswordValidator CheckPasswordHasAtLeastOneLowercaseLetter() => Failed;
        public override PasswordValidator CheckPasswordHasAtLeastOneCapitalLetter() => Failed;
    }
}

