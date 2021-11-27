namespace Aml.Models.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^\\d{10}$")]
        public string CompanyNumbers { get; set; }

        [Required]
        [EnumDataType(typeof(CompanyType))]
        public CompanyType CompanyType { get; set; }

        [Required]
        [EnumDataType(typeof(Market))]
        public Market Market { get; set; }

        public virtual IEnumerable<Notification> Notifications { get; set; }
    }
}