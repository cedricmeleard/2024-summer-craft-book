using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace PasswordValidation.Tests;

public class PasswordTests
{
    [Fact]
    public void StrongPassword_ShouldBeValid()
    {
        var entry = "passWord";
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
                && entry.Any(char.IsUpper)  // Must contains Capital letter
                );
    }
    
    # region Password defects
    
    [Theory]
    [InlineData("aaa\\018\\006")]
    [InlineData("\\026\\0162(t9[Q\\029")]
    public void PasswordWithEscapes_ShouldNotBe_Valid(string? entry)
    {
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.False(result);
        Assert.Null(password);
    }
    
    #endregion
}