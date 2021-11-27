namespace Aml.Models.Api.CompanyController
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCompanyRequestDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^\\d{10}$")]
        public string CompanyNumber { get; set; }

        [Required]
        [EnumDataType(typeof(CompanyType))]
        public CompanyType CompanyType { get; set; }

        [Required]
        [EnumDataType(typeof(Market))]
        public Market Market { get; set; }
    }
}
