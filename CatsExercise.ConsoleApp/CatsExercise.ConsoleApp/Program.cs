using System.IO;
using Microsoft.Extensions.Configuration;

namespace CatsExercise.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();
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
