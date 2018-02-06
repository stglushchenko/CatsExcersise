using System.Threading.Tasks;

namespace CatsExercise.Interfaces.Workflows
{
    public interface IWorkflow
    {
        Task<string> Run();
    }
}
