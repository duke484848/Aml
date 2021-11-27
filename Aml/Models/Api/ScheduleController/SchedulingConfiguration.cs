namespace Aml.Models.Api.ScheduleController
{
    using System.Collections.Generic;

    public class SchedulingConfiguration : ISchedulingConfiguration
    {
        public IEnumerable<SchedulingRule> Rules { get; set; }
    }

}
