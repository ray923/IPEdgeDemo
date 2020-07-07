using IPedgeProject.Data;
using IPedgeProject.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IPedgeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmpolyeeService _service;
        public EmployeeController(IEmpolyeeService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                List<Employee> allEmployees = _service.GetAllEmployee();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{pageIndex}/{pagesize}")]
        public IActionResult GetEmployees(int pageIndex,int pagesize)
        {
            try
            {
                PagedEmployees allEmployees = _service.GetPagedEmployee(pageIndex,pagesize);
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("SingleEmployee/{id}")]
        public IActionResult GetEmploeebyNumber(int id)
        {
            var employee = _service.GetEmploeebyNumber(id);
            return Ok(employee);
        }
        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody]Employee employee)
        {
            if(employee != null)
            {
                _service.AddEmployee(employee);
            }
            return Ok();
        }
        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmpolyee(int id, [FromBody]Employee employee)
        {
            _service.UpdateEmpolyee(id, employee);
            return Ok(employee);
        }
        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _service.DeleteEmployee(id);
            return Ok();
        }
        
    }
}
