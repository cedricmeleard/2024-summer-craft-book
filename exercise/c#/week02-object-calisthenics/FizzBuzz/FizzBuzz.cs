namespace FizzBuzz;

public static class FizzBuzz
{
    public static string Convert(int input)
    {
        input.EnsureInputValid();

        if (input.IsFizzBuzz()) return Constants.Fizzbuzz;
            
        if (input.IsFizz()) return Constants.Fizz;
            
        return input.IsBuzz()
            ? Constants.Buzz
            : input.ToString();
    }
}