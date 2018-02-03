using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatsExercise.Interfaces.Services
{
    public interface IEntityService<T> where T: class
    {
        Task<IEnumerable<T>> All();
    }
}
