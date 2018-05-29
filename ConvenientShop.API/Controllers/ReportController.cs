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
        private readonly IProductRepository _prepo;

        public ReportController(IReportRepository repo, IProductRepository prepo)
        {
            this._repo = repo;
            this._prepo = prepo;
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

        [HttpGet("importexport")]
        public IActionResult GetDataForImportingExportingReport(string time, char timespan = 'd', int productId = -1)
        {
            if (!DateTime.TryParseExact(time, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return BadRequest();
            }
            if (!_prepo.ProductExists(productId))
                return NotFound();
            return Ok(_repo.GetDataForImportExport(timespan, date, productId));
        }
    }
}