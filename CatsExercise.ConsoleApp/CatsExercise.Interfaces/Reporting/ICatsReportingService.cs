using CatsExercise.Models;
using System.Collections.Generic;
using System.Linq;

namespace CatsExercise.Interfaces.Reporting
{
    public interface ICatsReportingService
    {
        /// <summary>
        /// get all names of the cats in alphabetical order grouped by of the gender of their owner
        /// </summary>
        /// <returns></returns>
        ILookup<string, string> GroupCatNamesByOwnerGender(IEnumerable<Owner> owners);
    }
}
