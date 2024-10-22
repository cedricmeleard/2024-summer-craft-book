namespace FizzBuzz;

public static class FizzBuzzExtension
{
    public static bool IsBuzz(this int input) => input % 5 == 0;
    public static bool IsFizz(this int input) => input % 3 == 0;
    public static bool IsFizzBuzz(this int input) => input % 3 == 0 && input % 5 == 0;
    public static void EnsureInputValid(this int input)
    {
        if (input is <= 0 or > 100) {
            throw new OutOfRangeException();
        }
    }
}