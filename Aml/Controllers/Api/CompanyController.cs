namespace Aml.Controllers.Api
{
    using System.Net;
    using System.Threading.Tasks;
    using Aml.Data;
    using Aml.Models.Api.CompanyController.Dto;
    using Aml.Models.Api.Mappers;
    using Aml.Services;
    using Microsoft.AspNetCore.Mvc;
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
        /// <response code="200">Company exists, returning with no changes.</response>
        /// <response code="201">Company was created.</response>
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
