﻿using FizzBuzz.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = BuildHost();
await host.StartAsync();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Logger is a better logging tool than Console.WriteLine line but for this simple program Console.WriteLine works
Console.WriteLine($"Using environment: {environment}");
Console.WriteLine("Welcome to the amazing Galen Healthcare FizzBuzz.App program!");

var runProgram = true;

while (runProgram)
{
    Console.WriteLine("Close the program by entering q or enter the number you would like to FizzBuzz.App up to:");
    var input = Console.ReadLine();
    
    if (input != null)
    {
        if (int.TryParse(input, out var numberToFizzBuzz))
        {
            if (numberToFizzBuzz > 0 && numberToFizzBuzz < 101)
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
            // Dependency injection
            // https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
            // https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
            // Transient objects are always different. A new instance is provided to every controller and service.
            // Scoped objects are the same within a request and are different across different requests.
            // Singleton objects are the same for every object and request. (THIS MAKES SENSE HERE BECAUSE WE DO NOT NEED TO RE INSTANCE THE SERVICE EVERY REQUEST)
            services.AddSingleton<IFizzBuzz, FizzBuzz.Services.FizzBuzz>();
        }).Build();
}