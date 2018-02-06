using CatsExercise.Interfaces.Reporting;
using CatsExercise.Interfaces.Services;
using CatsExercise.Interfaces.Workflows;
using CatsExercise.Models;
using Microsoft.Extensions.Logging;
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
            ILookupPrintingService lookupPrintingService,
            ILogger logger)
        {
            _ownerService = ownerService;
            _catsReportingService = catsReportingService;
            _lookupPrintingService = lookupPrintingService;
        }

        public async Task<string> Run()
        {
            var owners = await _ownerService.All();
            
            var ownersLookup = _catsReportingService.GroupCatNamesByOwnerGender(owners);

            var result = _lookupPrintingService.PrintItemsWithHyphens(ownersLookup);

            return result;
        }
    }
}
