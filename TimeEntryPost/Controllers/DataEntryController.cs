using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TimeEntryPost.IRepository;
using TimeEntryPost.Model;

namespace TimeEntryPost.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class DataEntryController : ControllerBase
    {
        private readonly IEmployeeDetailsRepository _employeeDetailsRepository;
        public DataEntryController(IEmployeeDetailsRepository employeeDetailsRepository)
        {
            _employeeDetailsRepository = employeeDetailsRepository;
        }

        [Route("{customerId}/{authToken}/attendanceCloudpostAttendance")]
        [HttpPost]
        public IActionResult Add([FromBody] List<EmployeeModel> model, int customerId, string authToken)
        {
            string token = "96DB47E1980C43828D1BDAD2ED73061B";
            bool IsAuthenticate  = authToken.Equals(token);
            if (model == null && IsAuthenticate)
            {
                return NotFound( "Request body is missing!");
            } else if (!IsAuthenticate)
            {
                return NotFound("Authentiation is Failed!");
            }

            var obj = JsonConvert.SerializeObject(model, Formatting.Indented);
            var result = _employeeDetailsRepository.AddToDatabase(model);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _employeeDetailsRepository.GetAllData();

            return Ok();
        }
    }
}
