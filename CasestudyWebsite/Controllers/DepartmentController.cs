using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasestudyWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger _logger;

        public DepartmentController(ILogger<DepartmentController> Logger)
        {
            _logger = Logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                DepartmentViewModel viewmodel = new DepartmentViewModel();
                List<DepartmentViewModel> allDivisions = viewmodel.GetAll();
                return Ok(allDivisions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }
    }
}