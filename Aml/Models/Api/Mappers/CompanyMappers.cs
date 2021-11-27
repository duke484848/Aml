namespace Aml.Models.Api.Mappers
{
    using System.Collections.Generic;
    using System.Linq;
    using Aml.Models.Api.CompanyController;

    public static class CompanyMappers
    {
        // Or Automapper for bigger project
        public static Company ToCompany(this CreateCompanyRequestDto dto) =>
            new Company
            {
                Id = dto.Id,
                CompanyNumber = dto.CompanyNumber,
                CompanyType = dto.CompanyType,
                Market = dto.Market,
                Name = dto.Name,
                Notifications = new List<Notification>()
            };

        public static CreateCompanyResponseDto ToCreateCompanyResponseDto(this Company company) =>
        new CreateCompanyResponseDto
        {
            CompanyId = company.Id,
            Notifications = company?.Notifications.Select(x => x.Date.ToShortDateString()).ToList() ?? new List<string>()
        };

    }
}
