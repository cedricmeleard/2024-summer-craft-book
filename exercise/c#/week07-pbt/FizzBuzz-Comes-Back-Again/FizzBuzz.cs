using LanguageExt;

namespace FizzBuzz;

public static class FizzBuzz
{
    private const int Min = 1;
    private const int Max = 100;

    private static readonly List<(int Key, string Value)> Mapping =
    [
        (15, "FizzBuzz"),
        (5, "Buzz"),
        (3, "Fizz")
    ];

    public static Option<string> Convert(int input)
        => IsOutOfRange(input)
            ? Option<string>.None
            : ConvertSafely(input);

    private static string ConvertSafely(int input)
        => Mapping
            .Where(p => Is(p.Key, input))
            .Select(p => p.Value)
            .FirstOrDefault(input.ToString());

    private static bool Is(int divisor, int input) => input % divisor == 0;

    private static bool IsOutOfRange(int input) => input is < Min or > Max;
}