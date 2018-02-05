using System.IO;
using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Workflows;
using CatsExercise.Models;
using Microsoft.Extensions.Configuration;
using Unity;

namespace CatsExercise.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            ConfigureContainerAndRun(configuration);
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        private static void ConfigureContainerAndRun(IConfiguration configuration)
        {
            var container = new UnityContainer()
                .RegisterInstance(configuration)
                .RegisterSingleton<IContainerConfigurator, Services.ContainerConfigurator>("servicesConfigurator")
                .RegisterSingleton<IContainerConfigurator, Reporting.ContainerConfigurator>("reportingConfigurator")
                .RegisterSingleton<IContainerConfigurator, Workflows.ContainerConfigurator>("workflowsConfigurator");

            var solutionConfigurators = container.ResolveAll<IContainerConfigurator>();
            foreach (var configurator in solutionConfigurators)
            {
                configurator.RegisterInternalImplementations(container);
            }

            var workflow = container.Resolve<IWorkflow>();
            workflow.Run().Wait();
        }
    }
}
