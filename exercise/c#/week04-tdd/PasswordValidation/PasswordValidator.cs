using System.Text.RegularExpressions;

namespace PasswordValidation;

public class PasswordValidator
{
    private const string EscapedSequencePattern = @"\\[0-9]{3}";
    
    private readonly bool _isValid;
    private readonly string? _entry;
    public bool IsValid() => _isValid;
    
    private PasswordValidator(string entry, bool isValid)
    {
        _entry = entry;
        _isValid = isValid;
    }
    public static PasswordValidator Init(string? entry) => new(entry, true);
    private static PasswordValidator Failed => new(String.Empty, false);
    private PasswordValidator Success() => new(_entry,true);
    
    public PasswordValidator CheckPasswordMatchRequiredLength()
    {
        if (string.IsNullOrEmpty(_entry)) 
            return Failed;
        
        int escapedSequenceCount = Regex.Matches(_entry, EscapedSequencePattern).Count;
        // Calculate real length considering escaped sequences as one character
        bool isRealLengthGreaterOrEqualToEight = _entry.Length - escapedSequenceCount * 3 + escapedSequenceCount >= 8;

        return escapedSequenceCount == 0 && isRealLengthGreaterOrEqualToEight  
            ? Success() 
            : Failed;
    }
    public PasswordValidator CheckPasswordHasAtLeastOneCapitalLetter()
    {
        if (string.IsNullOrEmpty(_entry)) 
            return Failed;

        return _entry.Any(char.IsUpper) 
            ? Success() 
            : Failed;
    }
    public PasswordValidator CheckPasswordHasAtLeastOneLowercaseLetter()
    {
        if (string.IsNullOrEmpty(_entry)) 
            return Failed;
        
        return _entry.Any(char.IsLower) 
            ? Success() 
            : Failed;
    }
}