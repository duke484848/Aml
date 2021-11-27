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
    using Aml.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Swashbuckle.AspNetCore.Annotations;

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AmlContext _context;
        private readonly ISchedulingService _schedulingService;


        public CompanyController(AmlContext context, ISchedulingService schedulingService)
        {
            _context = context;
            _schedulingService = schedulingService;
        }

        /// <summary>
        /// Creates or return company with its schedule.
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Company exists, returning with no changes.")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Company was created.")]
        public async Task<ActionResult> CreateOrGetCompany([FromBody] CreateCompanyRequestDto request)
        {
            var company = _context.Company.Find(request.Id);
            if (company != null)
            {
                return new OkObjectResult(company.ToCreateCompanyResponseDto());
            }

            company = request.ToCompany();

            company.Notifications = _schedulingService.GenerateNotificationSchedule(company);

            _context.Company.Add(company);
            await _context.SaveChangesAsync();

            return new ObjectResult(company.ToCreateCompanyResponseDto())
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }
    }
}
