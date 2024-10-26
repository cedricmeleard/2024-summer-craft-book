using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace PasswordValidation.Tests;

public class PasswordTests
{
    # region Password Length
    [Property]
    public Property Password_With_MoreThan8Characters_ShouldBe_Valid(string? entry)
    {
        var property = () =>
        {
             var result = Password.TryCreate(entry, out Password? password);
             return result;
        };
        return property.When(entry is { Length: >= 8 });
    }

    [Theory]
    [InlineData("aaa\\018\\006")]
    [InlineData("\\026\\0162(t9[Q\\029")]
    public void PasswordWithEscapes_ShouldNotBe_Valid(string? entry)
    {
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.False(result);
        Assert.Null(password);
    }
    
    [Fact]
    public void StrongPassword_ShouldBeValid()
    {
        var entry = "passowrd";
        var result = Password.TryCreate(entry, out Password? password);
        
        Assert.True(result);
        Assert.NotNull(password);
        Assert.Equal(entry, password.Value);
    }
    #endregion
}