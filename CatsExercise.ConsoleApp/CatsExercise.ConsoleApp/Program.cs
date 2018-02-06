using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CatsExercise.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var bootstrapper = new Bootstrapper();
            bootstrapper.ConfigureContainer(configuration);

            Console.Write(bootstrapper.Run().Result);
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}
