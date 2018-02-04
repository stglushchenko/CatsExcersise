using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatsExercise.Interfaces.Reporting
{
    public interface ILookupPrintingService
    {
        string PrintItemsWithHyphens(ILookup<string, string> lookup);
    }
}
