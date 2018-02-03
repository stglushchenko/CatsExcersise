using CatsExercise.Interfaces.Services;
using CatsExercise.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatsExercise.Services
{
    public class OwnerService : IEntityService<Owner>
    {
        public async Task<IEnumerable<Owner>> All()
        {
            throw new NotImplementedException();
        }
    }
}
