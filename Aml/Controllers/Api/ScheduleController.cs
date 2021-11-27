namespace Aml.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Aml.Data;
    using Aml.Models.Api;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ScheduleController : ControllerBase
    {
        private readonly AmlContext _context;

        public ScheduleController(AmlContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateSchedule(Guid id)
        {
            var company = _context.Find<Company>(id);
            company.Notifications = new List<Notification>();
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
