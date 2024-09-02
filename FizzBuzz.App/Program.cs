using FizzBuzz.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = BuildHost();
await host.StartAsync();
// Logger is a better logging tool than Console.WriteLine line but for this simple program Console.WriteLine works
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
Console.WriteLine($"Using environment: {environment}");
Console.WriteLine("Welcome to the amazing Galen Healthcare FizzBuzz.App program!");
Console.WriteLine("You can close the program at any time by typing Q.");

var runProgram = true;

while (runProgram)
{
    Console.WriteLine("Please enter the number you would like to FizzBuzz.App up to:");
    var input = Console.ReadLine();
    if (input != null)
    {
        if (int.TryParse(input, out var numberToFizzBuzz))
        {
            if (numberToFizzBuzz > 0 && numberToFizzBuzz < 100)
            {
                var fizzBuzzService = host.Services.GetService<IFizzBuzz>();
                var results = fizzBuzzService!.CalculateFizzBuzz(numberToFizzBuzz);
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }   
            }
            else
            {
                Console.WriteLine("Please enter a valid integer greater than 0 and no greater than 100.");
            }
        }
        else if (StringComparer.CurrentCultureIgnoreCase.Compare(input, "q") == 0)
        {
            Console.WriteLine("Closing the program... thanks for FizzBuzzing with us!");
            runProgram = false;
            await host.StopAsync();
        }
        else
        {
            Console.WriteLine("Invalid input detected.");
        }  
    }
    else
    {
        Console.WriteLine("No input detected.");
    }   
}

static IHost BuildHost()
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddSingleton<IFizzBuzz, FizzBuzz.Services.FizzBuzz>();
        }).Build();
}