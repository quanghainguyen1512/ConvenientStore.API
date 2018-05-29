using System;
using System.Globalization;
using ConvenientShop.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/reports")]
    public class ReportController : Controller
    {
        private readonly IReportRepository _repo;

        public ReportController(IReportRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet("revenue")]
        public IActionResult GetRevenue(string time, char timespan = 'd')
        {
            if (!DateTime.TryParseExact(time, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return BadRequest();
            }

            return Ok(_repo.GetRevenue(timespan, date));
        }
    }
}