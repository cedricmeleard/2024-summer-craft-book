using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace PasswordValidation.Tests;

public class PasswordTests
{
    [Fact]
    public void StrongPassword_ShouldBeValid()
    {
        var entry = "p4ssWord";
        
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.True(result);
        Assert.NotNull(password);
        Assert.Equal(entry, password.Value);
    }
    
    [Property]
    public Property Password_With_MoreThan8Characters_ShouldHave_A_CapitalLetter_ToBeValid(string? entry)
    {
        var property = () =>
        {
            var result = Password.TryCreate(entry, out Password? password);
            return result;
        };
        
        return property
            .When(
                entry is { Length: >= 8 }   // At least 8 chars length 
                && entry.Any(char.IsUpper)  // Must contains at least one Capital letter
                && entry.Any(char.IsLower)  // Must contains at least one Lowercase letter
                && entry.Any(char.IsDigit)  // Must contains a digit
                );
    }
    
    # region Password defects
    
    [Theory]
    [InlineData("aaa\\018\\006")]
    [InlineData("\\026\\0162(t9[Q\\029")]
    [InlineData("PASSWORD")]
    [InlineData("PaSSWORD")]
    public void PasswordThat_ShouldBe_Invalid(string? entry)
    {
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.False(result);
        Assert.Null(password);
    }
    
    #endregion
}