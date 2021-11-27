namespace Aml.Models.Api.CompanyController.Dto
{
    using System;
    using System.Collections.Generic;

    public class CreateCompanyResponseDto
    {
        public Guid CompanyId { get; set; }

        public IEnumerable<string> Notifications { get; set; }
    }
}
