namespace Aml.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Aml.Data;
    using Aml.Models.Api.CompanyController;
    using Aml.Models.Api.CompanyController.Dto;
    using Aml.Models.Api.Mappers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AmlContext _context;
        private readonly ISchedulingConfiguration _schedulingConfiguration;

        public CompanyController(AmlContext context, ISchedulingConfiguration schedulingConfiguration)
        {
            _context = context;
            _schedulingConfiguration = schedulingConfiguration;
        }

        /// <summary>
        /// Creates or return company with its schedule
        /// </summary>
        /// <returns><Returned value./returns>
        /// <response code ="200">Company exists</response>
        /// <response code ="201">Company was created</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateOrGetCompany([FromBody] CreateCompanyRequestDto request)
        {
            var company = _context.Company.Find(request.Id);
            if (company != null)
            {
                return new OkObjectResult(company.ToCreateCompanyResponseDto());
            }

            company = request.ToCompany();

            company.Notifications = new List<Notification>();
            var rule = _schedulingConfiguration.Rules.Where(x =>
                x.Market == request.Market &&
                x.CompanyTypes.Contains(request.CompanyType))
            .FirstOrDefault();

            if (rule is not null)
            {
                for (int i = 0, dateOffset = 1; i < rule.NumberOfRepetitions; i++, dateOffset += rule.Interval)
                {
                    if (i == 1)
                    {
                        dateOffset--;
                    }

                    company.Notifications.Add(new Notification { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(dateOffset) });
                }
            }

            _context.Company.Add(company);
            await _context.SaveChangesAsync();

            return new ObjectResult(company.ToCreateCompanyResponseDto())
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }
    }
}
