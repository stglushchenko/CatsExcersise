using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Services;
using CatsExercise.Models;
using Unity;

namespace CatsExercise.Services
{
    public class ContainerConfigurator: IContainerConfigurator
    {
        public void RegisterInternalImplementations(IUnityContainer container)
        {
            container.RegisterSingleton<IEntityService<Owner>, OwnerService>();
        }
    }
}
