using CatsExercise.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CatsExercise.Services
{
    public class OwnerService : BaseEntityService<Owner>
    {
        public OwnerService(IConfiguration configuration, ILogger logger): base(configuration, logger)
        {
        }
    }
}
