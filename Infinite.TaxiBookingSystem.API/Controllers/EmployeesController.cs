using Infinite.TaxiBookingSystem.API.Models;
using Infinite.TaxiBookingSystem.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _repository;
        private readonly IGetRepository<Employee> _getRepository;
        private readonly IEmployeeRepository _employeeRepository;
        

        public EmployeesController(IRepository<Employee> repository,IGetRepository<Employee> getRepository, IEmployeeRepository employeeRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetAllEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _getRepository.GetAll();
        }

        [HttpGet("GetEmployeeById/{id}",Name ="GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee =await _getRepository.GetById(id);
            if (employee!= null)
            {
                return Ok(employee);
            }
            return NotFound("Employee not found");
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(employee);
            return CreatedAtRoute("GetEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, employee);
            if(result != null)
            {
                return NoContent();
            }
            return NotFound("Employee Not Found");
        }
        
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _repository.Delete(id);
            if(result!= null)
            {
                return Ok();
            }
            return NotFound("Movie Not Found");
        }

        [HttpGet("GetDesignations")]
        public async Task<IActionResult> GetDesignations()
        {
            var designations = await _employeeRepository.GetDesignations();
            return Ok(designations);
        }

        //[HttpPost]
        //public async Task<IActionResult> TransferData([FromBody] Employee employee)
        //{
        //    // Retrieve data from source table and map it to target model
        //    Employee employee1 = new Employee
        //    {
        //        EmployeeId = User.EmployeeId
                
        //    };

        //    // Save data in target table
        //    _repository.User.Add(User);
        //    await _repository.SaveChangesAsync();

        //    // Return success status
        //    return Ok();
        //}

    }
}
