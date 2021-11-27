namespace Aml.Models.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        private readonly DateTime _scheduleStart;

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^\\d{10}$")]
        public string CompanyNumbers { get; set; }

        [Required]
        [EnumDataType(typeof(CompanyType))]
        public CompanyType CompanyType { get; set; }

        /// <summary>
        /// There is no information if the scheduling rules are permitted to change
        /// If yes I would save all the dates in the db to not affect old schedules
        /// If no, they can be calculated on the fly which I decided to do.
        /// </summary>
        //public virtual List<Notification> Notifications { get; set; }

        public virtual List<Notification> Notifications => null;
    }
}
