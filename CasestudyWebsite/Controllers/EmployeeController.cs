using System;
using System.Collections.Generic;
using System.Reflection;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasestudyWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;

        public EmployeeController(ILogger<EmployeeController> Logger)
        {
            _logger = Logger;
        }
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                viewmodel.Email = email;
                viewmodel.GetByEmail();
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); //something went wrong
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] EmployeeViewModel viewmodel)
        {
            try
            {
                int retVal = viewmodel.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "Employee " + viewmodel.Lastname + " Updated!" });
                    case -1:
                        return Ok(new { msg = "Employee " + viewmodel.Lastname + " Not Updated!" });
                    case -2:
                        return Ok(new { msg = "Data is stale for " + viewmodel.Lastname + ", Employee Not Updated!" });
                    default:
                        return Ok(new { msg = "Employee " + viewmodel.Lastname + " Not Updated!" });


                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = viewmodel.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }



        [HttpPost]
        public IActionResult Post(EmployeeViewModel viewmodel)
        {
            try
            {
                viewmodel.Add();
                return viewmodel.Id > 1
                    ? Ok(new { msg = "Employee " + viewmodel.Lastname + " added!" })
                    : Ok(new { msg = "Employee " + viewmodel.Lastname + " not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }



        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                viewmodel.Id = id;
                return viewmodel.Delete() == 1
                    ? Ok(new { msg = "Employee " + id + " deleted!" })
                    : Ok(new { msg = "Employee " + id + " not deleted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); //something went wrong
            }
        }
    }
}