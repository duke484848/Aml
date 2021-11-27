namespace Aml.Models.Api.ScheduleController
{
    using System.Collections.Generic;

    public interface ISchedulingConfiguration
    {
        public IEnumerable<SchedulingRule> Rules { get; set; }
    }

}
