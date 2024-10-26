using System.Text.RegularExpressions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using Random = System.Random;

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
    
    [Property(Arbitrary = new[] { typeof(PasswordGenerators) }, MaxTest = 100)]
    public Property Password_ThatMeetRequirement_ShouldBeValid(string entry)
    {
        var property = () =>
        {
            bool result = Password.TryCreate(entry, out Password? password);
            
            Assert.True(result);
            Assert.NotNull(password);
            Assert.Equal(entry, password.Value);
            
            return result;
        };
        
        return property.ToProperty();
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

public static class PasswordGenerators
{
    public static Arbitrary<string> PasswordArbitrary()
    {
        return Arb.From(Gen.Sized(size =>
        {
            var minSize = Math.Max(size, 8);
            
            var upperCaseGen = Gen.Elements("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
            var lowerCaseGen = Gen.Elements("abcdefghijklmnopqrstuvwxyz".ToCharArray());
            var digitGen = Gen.Elements("0123456789".ToCharArray());
            var specialCharGen = Gen.Elements("*#@$%&.".ToCharArray());
            var allAllowedGen = Gen.Elements("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789*#@$%&.".ToCharArray());

            return from upper in upperCaseGen
                from lower in lowerCaseGen
                from digit in digitGen
                from special in specialCharGen
                from rest in Gen.ListOf(minSize - 4, allAllowedGen)
                select new string(
                    Shuffle(new[] { upper, lower, digit, special }
                        .Concat(rest)
                        .ToArray()));
        }));
    }
    
    private static readonly Random random = new();
    private static char[] Shuffle(char[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
        return array;
    }
}
