namespace Aml.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Aml.Data;
    using Aml.Models.Api;
    using Aml.Models.Api.ScheduleController;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ScheduleController : ControllerBase
    {
        private readonly AmlContext _context;
        private readonly ISchedulingConfiguration _schedulingConfiguration;

        public ScheduleController(AmlContext context, ISchedulingConfiguration schedulingConfiguration)
        {
            _context = context;
            _schedulingConfiguration = schedulingConfiguration;
        }

        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateSchedule(Guid id)
        {
            var company = _context.Find<Company>(id);
            if (company == null)
            {
                return NotFound();
            }

            company.Notifications = new List<Notification>();
            var rule = _schedulingConfiguration.Rules.Where(x =>
                x.Market == company.Market &&
                x.CompanyTypes.Contains(company.CompanyType)
            ).FirstOrDefault();
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
