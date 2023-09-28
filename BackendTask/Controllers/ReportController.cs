using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Task.Entities;
using Task.Interfaces;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportDsl _IReportDsl;
        public ReportController(IReportDsl IReportDsl)
        {
            _IReportDsl = IReportDsl;
        }
        [HttpGet]
        [Route("RevenueReport")]
        public IActionResult RevenueReport([FromQuery] ReportFilter reportFilters)
        {
           var resutl = _IReportDsl.GetRevenueReport(reportFilters);
            return Ok(resutl);
        }
        [HttpGet]
        [Route("AppointmentReport")]
        public IActionResult AppointmentReport([FromQuery] ReportFilter reportFilters)
        {
            var resutl = _IReportDsl.GetAppointmentReport(reportFilters);
            return Ok(resutl);
        }
        [HttpGet]
        [Route("CustomerDemographics")]
        public IActionResult CustomerDemographics([FromQuery] ReportFilter reportFilters)
        {
            var resutl = _IReportDsl.GetCustomerDemographics(reportFilters);
            return Ok(resutl);
        }

    }

}
