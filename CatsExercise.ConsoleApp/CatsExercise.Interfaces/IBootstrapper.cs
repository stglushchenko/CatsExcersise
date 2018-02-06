using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CatsExercise.Interfaces
{
    public interface IBootstrapper
    {
        void ConfigureContainer(IConfiguration configuration);

        Task<string> Run();
    }
}
