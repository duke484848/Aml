namespace Aml.Models.Api
{
    using System;
    using System.Collections.Generic;

    public class CreateScheduleResponseDto
    {
        public Guid CompanyId { get; set; }

        public IEnumerable<Notification> Notifications { get; set; }
    }
}
