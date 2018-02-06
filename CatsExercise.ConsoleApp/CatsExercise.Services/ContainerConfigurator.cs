using CatsExercise.Interfaces.IoC;
using CatsExercise.Interfaces.Services;
using CatsExercise.Models;
using System.Diagnostics.CodeAnalysis;
using Unity;

namespace CatsExercise.Services
{
    [ExcludeFromCodeCoverage]
    public class ContainerConfigurator: IContainerConfigurator
    {
        public void RegisterInternalImplementations(IUnityContainer container)
        {
            container.RegisterSingleton<IEntityService<Owner>, OwnerService>();
        }
    }
}
