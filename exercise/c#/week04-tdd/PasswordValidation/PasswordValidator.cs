using System.Text.RegularExpressions;

namespace PasswordValidation;

public class PasswordValidator
{
    private const string EscapedSequencePattern = @"\\[0-9]{3}";
    
    private readonly bool _isValid;
    public bool IsValid() => _isValid;
    
    private PasswordValidator(bool isValid)
    {
        _isValid = isValid;
    }
    public static PasswordValidator Init() => new(true);
    private static PasswordValidator Failed => new(false);
    private static PasswordValidator Success => new(true);
    
    public PasswordValidator CheckPasswordMatchRequiredLength(string? entry)
    {
        if (string.IsNullOrEmpty(entry)) 
            return Failed;
        
        int escapedSequenceCount = Regex.Matches(entry, EscapedSequencePattern).Count;
        // Calculate real length considering escaped sequences as one character
        bool isRealLengthGreaterOrEqualToEight = entry.Length - escapedSequenceCount * 3 + escapedSequenceCount >= 8;

        return escapedSequenceCount == 0 && isRealLengthGreaterOrEqualToEight  
            ? Success 
            : Failed;
    }
}