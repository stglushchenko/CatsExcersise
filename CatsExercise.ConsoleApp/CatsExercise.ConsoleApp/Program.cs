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

            try
            {
                var result = bootstrapper.Run().Result;
                Console.Write(result);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            
            Console.ReadKey();
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
