using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Workflows;
using Unity;

namespace CatsExercise.Workflows
{
    public class ContainerConfigurator: IContainerConfigurator
    {
        public void RegisterInternalImplementations(IUnityContainer container)
        {
            container.RegisterSingleton<IWorkflow, MainWorkflow>();
        }
    }
}
