namespace Aml.Tests.Models.Api.CompanyController
{
    using Aml.Data;
    using Aml.Models.Api.CompanyController;
    using Aml.Models.Api.CompanyController.Dto;
    using Aml.Models.Api.Mappers;
    using Aml.Services;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Newtonsoft.Json;
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
            company.Notifications = new List<Notification>
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
                    };
            var contextMock = new Mock<AmlContext>();
            contextMock.Setup(x => x.Company.Find(It.IsAny<Guid>())).Returns(company);

            var sut = new Controller(contextMock.Object, null);

            //Act
            var actual = await sut.CreateOrGetCompany(request);

            //Assert
            var response = Assert.IsType<OkObjectResult>(actual);
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var actualStatusCode = response.StatusCode;
            Assert.Equal(expectedStatusCode, actualStatusCode);
            var expectedJSON = JsonConvert.SerializeObject(company.ToCreateCompanyResponseDto());
            var actualJSON = JsonConvert.SerializeObject(response.Value);
            Assert.Equal(expectedJSON, actualJSON);
        }

        [Fact]
        public async Task When_the_company_doesnt_exist_It_returns_201_status_code()
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


            var contextMock = new Mock<AmlContext>();
            contextMock.Setup(x => x.Company.Find(It.IsAny<Guid>())).Returns<Company>(null);

            var schedulingService = new Mock<ISchedulingService>();
            var expectedNotifications = new List<Notification>
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
                    };
            schedulingService.Setup(x => x.GenerateNotificationSchedule(It.IsAny<Company>()))
                .Returns(expectedNotifications);
            var expectedCompany = request.ToCompany();
            expectedCompany.Notifications = expectedNotifications;

            var sut = new Controller(contextMock.Object, schedulingService.Object);

            //Act
            var actual = await sut.CreateOrGetCompany(request);


            //Assert
            var response = Assert.IsType<ObjectResult>(actual);
            var expectedStatusCode = (int)HttpStatusCode.Created;
            var actualStatusCode = response.StatusCode;
            Assert.Equal(expectedStatusCode, actualStatusCode);
            var expectedJSON = JsonConvert.SerializeObject(expectedCompany.ToCreateCompanyResponseDto());
            var actualJSON = JsonConvert.SerializeObject(response.Value);
            Assert.Equal(expectedJSON, actualJSON);
        }

    }
}