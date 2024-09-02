namespace FizzBuzz.Services;

public class FizzBuzz: IFizzBuzz
{
    public IEnumerable<string> CalculateFizzBuzz(int numberToFizzBuzz)
    {
        if (numberToFizzBuzz > -1)
        {
            var results = new List<string>();
            foreach (var i in Enumerable.Range(1, numberToFizzBuzz))
            {
                var isFizz = i % 3 == 0;
                var isBuzz = i % 5 == 0;
                var isFizzBuzz = isFizz && isBuzz;
                if (isFizzBuzz)
                {
                    results.Add("FizzBuzz");
                }
                else if (isFizz)
                {
                    results.Add("Fizz");
                }
                else if(isBuzz)
                {
                    results.Add("Buzz");
                }
                else
                {
                    results.Add(i.ToString());
                }
            }
            return results;   
        }
        return [];
    }
}