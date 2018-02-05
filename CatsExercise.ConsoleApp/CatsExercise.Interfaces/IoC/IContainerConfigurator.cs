using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace CatsExercise.Interfaces.IoC
{
    public interface IContainerConfigurator
    {
        /// <summary>
        /// Registers implementations of public interfaces in the contained assembly 
        /// (composition root)
        /// </summary>
        /// <param name="container"></param>
        void RegisterInternalImplementations(IUnityContainer container);
    }
}
