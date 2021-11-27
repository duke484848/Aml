namespace Aml.Models.Api.CompanyController
{
    using System.Collections.Generic;

    public class SchedulingConfiguration : ISchedulingConfiguration
    {
        public IEnumerable<SchedulingRule> Rules { get; set; }
    }
}
