using CatsExercise.Interfaces;
using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Workflows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;
using Unity;

namespace CatsExercise.ConsoleApp
{
    public class Bootstrapper : IBootstrapper
    {
        private IUnityContainer _container;

        private string _defaultLogFileName = "Logs/CatsExercise-.log";
        private string _defaultLogCategoryName = "MainLog";

        public void ConfigureContainer(IConfiguration configuration)
        {
            var loggerFactory = ConfigureLogging();

            _container = new UnityContainer()
                .RegisterInstance(configuration)
                .RegisterInstance(loggerFactory.CreateLogger(_defaultLogCategoryName))
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
            try
            {
                var workflow = _container.Resolve<IWorkflow>();
                return await workflow.Run();
            }
            catch(Exception ex)
            {
                _container.Resolve<Microsoft.Extensions.Logging.ILogger>().LogCritical(ex,"The unexpexted exception occured");
                throw;
            }
        }

        private ILoggerFactory ConfigureLogging()
        {
            var minimumLogLevel = LogEventLevel.Information;
#if DEBUG
            minimumLogLevel = LogEventLevel.Debug;
#endif
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: minimumLogLevel)
                .WriteTo.File(_defaultLogFileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog(dispose: true);
            return loggerFactory;
        }
    }
}
