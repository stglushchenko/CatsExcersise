using CatsExercise.Models;
using Microsoft.Extensions.Configuration;

namespace CatsExercise.Services
{
    public class OwnerService : BaseEntityService<Owner>
    {
        public OwnerService(IConfiguration configuration): base(configuration)
        {
        }
    }
}
