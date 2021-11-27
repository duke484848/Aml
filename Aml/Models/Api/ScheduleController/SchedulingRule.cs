namespace Aml.Models.Api.ScheduleController
{
    using System.Collections.Generic;

    public class SchedulingRule
    {
        public Market Market { get; set; }

        public int Interval { get; set; }

        public int NumberOfRepetitions { get; set; }

        public IEnumerable<CompanyType> CompanyTypes { get; set; }
    }
}