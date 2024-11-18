using FsCheck;
using FsCheck.Xunit;
using Moq;

namespace AllProperties.Tests;

public class FunctionsTests
{
    private readonly Functions _functions;
    private readonly Mock<ITaskDelayer> _mock;

    public FunctionsTests()
    {
        _mock = new Mock<ITaskDelayer>();
        _functions = new Functions(_mock.Object);
    }

    #region Sort

    [Property]
    public void Sort_should_correctly_order()
    {
        Prop.ForAll(
            Arb.From<int[]>(),
            array =>
            {
                int[] sorted = _functions.Sort(array);
                for (int i = 0; i < sorted.Length - 1; i++) Assert.True(sorted[i] <= sorted[i + 1]);
            }).QuickCheckThrowOnFailure();
    }

    #endregion

    #region FilterEvenNumbers

    [Property]
    public void FilterEventNumbers_should_return_only_even_numbers()
    {
        Prop.ForAll(
                Arb.From<int[]>(),
                array
                    => Assert.All(
                        _functions.FilterEvenNumbers(array),
                        value => Assert.True(int.IsEvenInteger(value))))
            .QuickCheckThrowOnFailure();
    }

    #endregion

    #region LoadAsyncData

    [Property]
    public void LoadAsyncData_should_return_data_from_url()
    {
        _mock.Setup(x => x.Delay(It.IsAny<int>())).Returns(Task.CompletedTask);

        Prop.ForAll(
            Arb.From<string>(),
            async url =>
            {
                string result = await _functions.LoadDataAsync(url);
                Assert.Equal("Data from " + url, result);
            }).QuickCheckThrowOnFailure();
    }

    [Fact]
    public void LoadAsyncData_should_return_failed_when_an_error_happen()
    {
        _mock.Setup(x => x.Delay(It.IsAny<int>())).Throws<Exception>();

        Prop.ForAll(
            Arb.From<string>(),
            async url =>
            {
                string result = await _functions.LoadDataAsync(url);
                Assert.Equal("Failed to load", result);
            }).QuickCheckThrowOnFailure();
    }

    #endregion

    #region TimString

    [Property]
    public void TrimString_should_remove_leading_and_trailing_spaces()
    {
        Prop.ForAll(
                Arb.From<string>(),
                s => (!string.IsNullOrEmpty(s))
                    .Implies(() => Assert.Equal(s.Trim(), _functions.TrimString(s))))
            .QuickCheckThrowOnFailure();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void TrimString_should_return_emtpy_string_when_string_is_null_or_empty(string? s)
    {
        Assert.Equal(string.Empty, _functions.TrimString(s));
    }

    #endregion

    #region Max

    [Property]
    public void Max_commutativity()
    {
        Prop.ForAll(
            Arb.From<int>(),
            Arb.From<int>(),
            (a, b) => Assert
                .Equal(
                    _functions.Max(a, b),
                    _functions.Max(b, a))
        ).QuickCheckThrowOnFailure();
    }

    [Property]
    public void Max_when_a_lower_or_equal_than_b_should_return_b()
    {
        Prop.ForAll(
            Arb.From<int>(),
            Arb.From<int>(),
            (a, b) =>
                (a <= b).Implies(() => { Assert.Equal(b, _functions.Max(a, b)); })
        ).QuickCheckThrowOnFailure();
    }

    #endregion
}