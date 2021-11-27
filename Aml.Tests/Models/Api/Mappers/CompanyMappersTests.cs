namespace Aml.Tests.Models.Api.CompanyController
{
    using Aml.Models.Api.CompanyController;
    using Aml.Models.Api.CompanyController.Dto;
    using Aml.Models.Api.Mappers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Xunit;

    public class CompanyMapperTests
    {

        [Fact]
        public void ToCompany_returns_correct_company_object()
        {
            var id = Guid.NewGuid();
            var actual = new CreateCompanyRequestDto()
            {
                Id = id,
                CompanyNumber = "1234567890",
                CompanyType = CompanyType.Large,
                Market = Market.Denmark,
                Name = "Abc",
            }.ToCompany();

            var expected = new Company()
            {
                Id = id,
                CompanyNumber = "1234567890",
                CompanyType = CompanyType.Large,
                Market = Market.Denmark,
                Name = "Abc",
                Notifications = new List<Notification>()
            };


            var expectedSerialized = JsonConvert.SerializeObject(expected);
            var actualSerialized = JsonConvert.SerializeObject(actual);
            Assert.Equal(expectedSerialized, actualSerialized);
        }

        [Fact]
        public void ToCreateCompanyResponseDto_creates_correct_response_from_company_object()
        {
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                CompanyNumber = "1234567890",
                CompanyType = CompanyType.Large,
                Market = Market.Denmark,
                Name = "Abc",
                Notifications = new List<Notification>
                {
                    new Notification
                    {
                        Date = DateTime.Now
                    },
                       new Notification
                    {
                        Date = DateTime.Now.AddDays(10)
                    },
                }
            };
            var actual = company.ToCreateCompanyResponseDto();
            Assert.Equal(company.Id, actual.CompanyId);
            var expected = company.Notifications.Select(x => x.Date.ToShortDateString()).ToList();
            Assert.Equal(expected, actual.Notifications);

        }
    }
}