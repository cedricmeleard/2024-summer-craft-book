using FluentAssertions.LanguageExt;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace FizzBuzz;

public class FizzBuzzTests
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(67, "67")]
    [InlineData(82, "82")]
    [InlineData(3, "Fizz")]
    [InlineData(66, "Fizz")]
    [InlineData(99, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(50, "Buzz")]
    [InlineData(85, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    [InlineData(30, "FizzBuzz")]
    [InlineData(45, "FizzBuzz")]
    [InlineData(60, "FizzBuzz")]
    [InlineData(75, "FizzBuzz")]
    public void Returns_Number_Representation(int input, string expectedResult)
        => FizzBuzz.Convert(input)
            .Should().Be(expectedResult);

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    [InlineData(-1)]
    public void Fails_For_Numbers_Out_Of_Range(int input)
        => FizzBuzz.Convert(input)
            .Should()
            .BeNone();


    [Property(MaxTest = 100)]
    public Property Verify_Fizz_Case()
    {
        var generator = Gen.Choose(1, 100)
            .Where(value => value % 3 == 0 && value % 5 != 0);

        return Prop.ForAll(generator.ToArbitrary(),
            value
                => FizzBuzz.Convert(value).Should().Be("Fizz"));
    }

    [Property(MaxTest = 100)]
    public Property Verify_Buzz_Case()
    {
        var generator = Gen.Choose(1, 100)
            .Where(value => value % 3 != 0 && value % 5 == 0);

        return Prop.ForAll(generator.ToArbitrary(),
            value
                => FizzBuzz.Convert(value).Should().Be("Buzz"));
    }

    [Property(MaxTest = 100)]
    public Property Verify_FizzBuzz_Case()
    {
        var generator = Gen.Choose(1, 100)
            .Where(value => value % 15 == 0);

        return Prop.ForAll(generator.ToArbitrary(),
            value
                => FizzBuzz.Convert(value).Should().Be("FizzBuzz"));
    }

    [Property(MaxTest = 100)]
    public Property Verify_NeitherFizzOrBuzz_Case()
    {
        var generator = Gen.Choose(1, 100)
            .Where(value => value % 3 != 0 && value % 5 != 0);

        return Prop.ForAll(generator.ToArbitrary(),
            value
                => FizzBuzz.Convert(value).Should().Be(value.ToString()));
    }
}