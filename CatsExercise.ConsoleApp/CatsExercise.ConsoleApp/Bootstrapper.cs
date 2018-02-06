using CatsExercise.Interfaces;
using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Workflows;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Unity;

namespace CatsExercise.ConsoleApp
{
    public class Bootstrapper : IBootstrapper
    {
        private IUnityContainer _container;

        public void ConfigureContainer(IConfiguration configuration)
        {
            _container = new UnityContainer()
                .RegisterInstance(configuration)
                .RegisterSingleton<IContainerConfigurator, Services.ContainerConfigurator>("servicesConfigurator")
                .RegisterSingleton<IContainerConfigurator, Reporting.ContainerConfigurator>("reportingConfigurator")
                .RegisterSingleton<IContainerConfigurator, Workflows.ContainerConfigurator>("workflowsConfigurator");

            var solutionConfigurators = _container.ResolveAll<IContainerConfigurator>();
            foreach (var configurator in solutionConfigurators)
            {
                configurator.RegisterInternalImplementations(_container);
            }
        }

        public async Task<string> Run()
        {
            var workflow = _container.Resolve<IWorkflow>();
            return await workflow.Run();
        }
    }
}
