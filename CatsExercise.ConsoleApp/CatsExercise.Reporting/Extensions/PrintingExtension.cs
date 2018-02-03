using System.Linq;
using System.Text;

namespace CatsExercise.Reporting.Extensions
{
    public static class PrintingExtension
    {
        public static string ToFormattedResult(this ILookup<string, string> lookup)
        {
            var stringBuilder = new StringBuilder();

            foreach(var grouping in lookup)
            {
                stringBuilder.Append(grouping.Key);

                foreach (var item in grouping)
                {
                    stringBuilder.Append($"  -{item}");
                }
            }

            return stringBuilder.ToString();

        }
    }
}
