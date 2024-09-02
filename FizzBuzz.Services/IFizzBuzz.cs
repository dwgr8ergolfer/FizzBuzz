namespace FizzBuzz.Services;

public interface IFizzBuzz
{
    public IEnumerable<string> CalculateFizzBuzz(int numberToFizzBuzz);
}