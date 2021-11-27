namespace Aml.Models.Api.CompanyController
{
    using System.Collections.Generic;

    public interface ISchedulingConfiguration
    {
        public IEnumerable<SchedulingRule> Rules { get; set; }
    }
}
