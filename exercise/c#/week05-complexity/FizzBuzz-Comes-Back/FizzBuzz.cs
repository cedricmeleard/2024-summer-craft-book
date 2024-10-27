namespace FizzBuzz
{
    public static class FizzBuzz
    {
        private const int Min = 0;
        private const int Max = 100;
        private const int Fizz = 3;
        private const int Buzz = 5;
        private const int Fizz_Buzz = 15;

        private static List<KeyValuePair<int, string>> map = new()
        {
            new(Fizz_Buzz, "FizzBuzz"),
            new(Fizz, "Fizz"),
            new(Buzz, "Buzz"),
        };

        public static string Convert(int input) => map
            .Where(_ => IsOutOfRange(input) ? throw new OutOfRangeException() : true)
            .Where(predicate => Is(predicate.Key, input))
            .Select(predicate => predicate.Value)
            .FirstOrDefault() ?? input.ToString();
        
        private static bool Is(int divisor, int input) => input % divisor == 0;
        private static bool IsOutOfRange(int input) => input is <= Min or > Max;
    }
}