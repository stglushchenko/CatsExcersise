using CatsExercise.Models;
using CatsExercise.Interfaces.Reporting;
using System.Collections.Generic;
using System.Linq;
using static CatsExercise.Models.Enums.PetType;
using System;
using CatsExercise.Models.Enums;

namespace CatsExercise.Reporting
{
    public class CatsReportingService : ICatsReportingService
    {
        public ILookup<string, string> GroupCatNamesByOwnerGender(IEnumerable<Owner> owners)
        {
            return owners
                .Where(owner => owner.Pets != null && owner.Pets.Any())
                .SelectMany(owner => owner.Pets,
                    (owner, pet) => new { owner, pet })
                .Where(ownerAndPet => ownerAndPet.pet.PetType == Cat)
                .OrderBy(ownerAndPet => ownerAndPet)
                .ToLookup(ownerAndPet => Enum.GetName(typeof(Gender), ownerAndPet.owner.Gender),
                    ownerAndPet => ownerAndPet.pet.Name);
                
        }
    }
}
