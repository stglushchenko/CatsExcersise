using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Reporting;
using System.Diagnostics.CodeAnalysis;
using Unity;

namespace CatsExercise.Reporting
{
    [ExcludeFromCodeCoverage]
    public class ContainerConfigurator: IContainerConfigurator
    {
        public void RegisterInternalImplementations(IUnityContainer container)
        {
            container.RegisterSingleton<ICatsReportingService, CatsReportingService>()
                     .RegisterSingleton<ILookupPrintingService, LookupPrintingService>();
        }
    }
}
