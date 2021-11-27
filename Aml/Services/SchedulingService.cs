using Aml.Models.Api.CompanyController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aml.Services
{
    public class SchedulingService : ISchedulingService
    {

        private readonly ISchedulingConfiguration _schedulingConfiguration;

        public SchedulingService(ISchedulingConfiguration schedulingConfiguration)
        {
            _schedulingConfiguration = schedulingConfiguration;
        }

        public ICollection<Notification> GenerateNotificationSchedule(Company company)
        {
            var rule = _schedulingConfiguration.Rules.Where(x =>
              x.Market == company.Market &&
              x.CompanyTypes.Contains(company.CompanyType)).FirstOrDefault();

            var notifications = new List<Notification>();
            if (rule is not null)
            {
                for (int i = 0, dateOffset = 1; i < rule.NumberOfRepetitions; i++, dateOffset += rule.Interval)
                {
                    if (i == 1)
                    {
                        dateOffset--;
                    }

                    notifications.Add(new Notification { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(dateOffset) });
                }
            }

            return notifications;
        }

    }
}
