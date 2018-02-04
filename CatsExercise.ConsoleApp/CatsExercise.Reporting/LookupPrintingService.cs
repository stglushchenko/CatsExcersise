using System;
using System.Linq;
using System.Text;
using CatsExercise.Interfaces.Reporting;

namespace CatsExercise.Reporting
{
    public class LookupPrintingService: ILookupPrintingService
    {
        public string PrintItemsWithHyphens(ILookup<string, string> lookup)
        {
            if (lookup == null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            var stringBuilder = new StringBuilder();

            foreach(var grouping in lookup)
            {
                stringBuilder.AppendLine(grouping.Key);

                foreach (var item in grouping)
                {
                    stringBuilder.AppendLine($"  -{item}");
                }
            }

            return stringBuilder.ToString();

        }
    }
}
