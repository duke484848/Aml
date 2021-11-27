namespace Aml.Tests.Models.Api.CompanyController
{
    using Aml.Data;
    using Aml.Models.Api.CompanyController;
    using Aml.Models.Api.CompanyController.Dto;
    using Aml.Models.Api.Mappers;
    using Aml.Services;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;
    using Controller = Aml.Controllers.Api.CompanyController;
    public class CompanyControllerTests
    {
        [Fact]
        public async Task When_the_company_exists_It_returns_200_status_code()
        {
            //Arrange
            var request = new CreateCompanyRequestDto()
            {
                Id = Guid.NewGuid(),
                CompanyNumber = "1234567890",
                CompanyType = CompanyType.Large,
                Market = Market.Denmark,
                Name = "Abc",
            };

            var company = request.ToCompany();

            var contextMock = new Mock<AmlContext>();
            contextMock.Setup(x => x.Find(It.IsAny<Guid>())).Returns(company);
            var schedulingService = new Mock<ISchedulingService>();
            schedulingService.Setup(x => x.GenerateNotificationSchedule(It.IsAny<Company>())).Returns(
                new List<Notification>
                    {
                        new Notification{
                            Date =  DateTime.Now.AddDays(1)
                        },
                        new Notification{
                            Date =  DateTime.Now.AddDays(5)
                        },
                       new Notification{
                            Date =  DateTime.Now.AddDays(10)
                        }
                    }
                );




            var sut = new Controller(contextMock.Object, schedulingService.Object);

            //Act
            var actual = sut.CreateOrGetCompany(request);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actual);
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var actualStatusCode = result.StatusCode;
            Assert.Equal(expectedStatusCode, actualStatusCode);
            var actualValue = result.Value;
            var expectedValue = company.ToCreateCompanyResponseDto().Notifications;
            Assert.Equal(expectedValue, actualValue);
            //var expected = company.Notifications.Select(x => x.Date.ToShortDateString()).ToList();
            //Assert.Equal(expected, actual.Notifications);
        }

        [Fact]
        public void When_the_company_doesnt_exist_It_returns_201_status_code()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}