namespace Aml.Models.Api
{
    using System;
    using System.Collections.Generic;

    public class CreateCompanyResponseDto
    {
        public Guid CompanyId { get; set; }

        public IEnumerable<string> Notifications { get; set; }
    }
}
