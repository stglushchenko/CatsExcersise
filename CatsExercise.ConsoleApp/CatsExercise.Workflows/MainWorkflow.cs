using CatsExercise.Interfaces.Reporting;
using CatsExercise.Interfaces.Services;
using CatsExercise.Interfaces.Workflows;
using CatsExercise.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatsExercise.Workflows
{
    public class MainWorkflow : IWorkflow
    {
        private readonly IEntityService<Owner> _ownerService;
        private readonly ICatsReportingService _catsReportingService;
        private readonly ILookupPrintingService _lookupPrintingService;

        public MainWorkflow(IEntityService<Owner> ownerService,
            ICatsReportingService catsReportingService, 
            ILookupPrintingService lookupPrintingService)
        {
            _ownerService = ownerService;
            _catsReportingService = catsReportingService;
            _lookupPrintingService = lookupPrintingService;
        }

        public async Task Run()
        {
            var owners = await _ownerService.All();

            var ownersLookup = _catsReportingService.GroupCatNamesByOwnerGender(owners);

            var result = _lookupPrintingService.PrintItemsWithHyphens(ownersLookup);

            Console.Write(result);

            Console.ReadKey();
        }
    }
}
