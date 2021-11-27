namespace Aml.Services
{
    using System.Collections.Generic;
    using Aml.Models.Api.CompanyController;

    public interface ISchedulingService
    {
        public ICollection<Notification> GenerateNotificationSchedule(Company company);
    }
}