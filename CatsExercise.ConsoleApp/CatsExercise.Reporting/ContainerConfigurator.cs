using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Reporting;
using Unity;

namespace CatsExercise.Reporting
{
    public class ContainerConfigurator: IContainerConfigurator
    {
        public void RegisterInternalImplementations(IUnityContainer container)
        {
            container.RegisterSingleton<ICatsReportingService, CatsReportingService>()
                     .RegisterSingleton<ILookupPrintingService, LookupPrintingService>();
        }
    }
}
