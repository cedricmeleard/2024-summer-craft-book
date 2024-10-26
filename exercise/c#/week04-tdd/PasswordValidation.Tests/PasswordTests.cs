using System.Text.RegularExpressions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace PasswordValidation.Tests;

public class PasswordTests
{
    [Fact]
    public void StrongPassword_ShouldBeValid()
    {
        var entry = "p4ssW@rd";
        
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.True(result);
        Assert.NotNull(password);
        Assert.Equal(entry, password.Value);
    }
    
    [Property]
    public Property Password_ThatMeetRequirement_ShoulBeValid(string? entry)
    {
        var property = () =>
        {
            var result = Password.TryCreate(entry, out Password? password);
            
            Assert.True(result);
            Assert.NotNull(password);
            Assert.Equal(entry, password.Value);
            
            return result;
        };
        var pattern = @"^[a-zA-Z0-9.*#@$%&]+$";
        
        return property.When(entry is { Length: >= 8 }                       // At least 8 chars length 
                             && entry.Any(char.IsUpper)                      // Must contain at least one Capital letter
                             && entry.Any(char.IsLower)                      // Must contain at least one Lowercase letter
                             && entry.Any(char.IsDigit)                      // Must contains a digit
                             && entry.Any(c => "*#@$%&.".Contains(c))    // At least one of * # @ $ % &.
                             // Should be chars (lower or upper), digit or one of * # @ $ % &
                             && Regex.IsMatch(entry, pattern)
                             );
    }
    
    # region Password defects
    
    [Theory]
    [InlineData("aaa\\018\\006")]
    [InlineData("\\026\\0162(t9[Q\\029")]
    [InlineData("PASSWORD")]
    [InlineData("PaSSWORD")]
    [InlineData("PaSSWO4D")]
    [InlineData("PaSSµO4D")]
    [InlineData("$g,N\\026\\016UwC9~ \\022b")]
    [InlineData("9hzp<^#\025e\007}B")]
    public void PasswordThat_ShouldBe_Invalid(string? entry)
    {
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.False(result);
        Assert.Null(password);
    }
    
    #endregion
}