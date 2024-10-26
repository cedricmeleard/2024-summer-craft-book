using System.Text.RegularExpressions;

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
        // Detect escaped sequences
        var escapedSequencePattern = @"\\[0-9]{3}";
        int escapedSequenceCount = Regex.Matches(entry, escapedSequencePattern).Count;

        // Calculate real length considering escaped sequences as one character
        int realLength = entry.Length - escapedSequenceCount * 3 + escapedSequenceCount;

        // Return true if there are no escaped sequences and real length is at least 8
        bool hasNoEscapedSequences = !Regex.IsMatch(entry, escapedSequencePattern);
        var result = hasNoEscapedSequences && realLength >= 8;
        if (result) {
            password = new Password(entry);
            return true;
        }
        
        password = null;
        return false;
    }
}