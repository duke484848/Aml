﻿namespace Aml.Tests.Models.Api.CompanyController
{
    using Aml.Models.Api.CompanyController;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class CompanyTests
    {
        public static IEnumerable<object[]> InvalidCases
        {
            get
            {
                yield return new object[] {
                    new Company()
                    {
                        Id = Guid.NewGuid(),
                        CompanyNumber = "not valid",
                        CompanyType = CompanyType.Large,
                        Market = Market.Denmark,
                        Name = "Abc",
                    }
                };
                yield return new object[] {
                    new Company()
                    {
                        Id = Guid.NewGuid(),
                        CompanyType = CompanyType.Large,
                        Market = Market.Denmark,
                        Name = "Abc",
                    }
                };
                yield return new object[] {
                    new Company()
                    {
                        Id = Guid.NewGuid(),
                        CompanyNumber = "1234567890",
                        Market = Market.Denmark,
                        Name = "Abc",
                    }
                };
                yield return new object[] {
                    new Company()
                    {
                        Id = Guid.NewGuid(),
                        CompanyNumber = "1234567890",
                        CompanyType = CompanyType.Large,
                        Name = "Abc",
                    }
                };
                yield return new object[] {
                    new Company()
                    {
                        Id = Guid.NewGuid(),
                        CompanyNumber = "1234567890",
                        CompanyType = CompanyType.Large,
                        Market = Market.Denmark,
                    }
                };
            }
        }

        [Fact]
        public void When_I_provide_valid_values_the_model_is_valid()
        {
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                CompanyNumber = "1234567890",
                CompanyType = CompanyType.Large,
                Market = Market.Denmark,
                Name = "Abc",
            };
            var context = new ValidationContext(company, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(company, context, results, true);
            Assert.True(isValid);
        }


        [Theory]
        [MemberData(nameof(InvalidCases))]
        public void When_I_provide_invalid_values_the_model_is_invalid(Company company)
        {
            var context = new ValidationContext(company, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(company, context, results, true);
            Assert.False(isValid);
        }
    }
}