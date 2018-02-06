using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Workflows;
using System.Diagnostics.CodeAnalysis;
using Unity;

namespace CatsExercise.Workflows
{
    public class ContainerConfigurator: IContainerConfigurator
    {
        [ExcludeFromCodeCoverage]
        public void RegisterInternalImplementations(IUnityContainer container)
        {
            container.RegisterSingleton<IWorkflow, MainWorkflow>();
        }
    }
}
