using Infinite.TaxiBookingSystem.API.Models;
using Infinite.TaxiBookingSystem.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly ApplicationDbContext _dbContext;
        private readonly IBookingsStatusRepository<Booking> _bookingsStatusRepository;
        

        public EmployeesController(IRepository<Employee> repository,IGetRepository<Employee> getRepository, IEmployeeRepository employeeRepository,ApplicationDbContext dbContext, IBookingsStatusRepository<Booking> bookingsStatusRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
            _employeeRepository = employeeRepository;
            _dbContext = dbContext;
            _bookingsStatusRepository = bookingsStatusRepository;
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

        [Authorize(Roles ="Employee,Admin")]
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(employee);
            int id = employee.EmployeeId;
            var loginId = User.FindFirstValue(ClaimTypes.Name);

            var userinDb = _dbContext.Users.FirstOrDefault(x => x.LoginID == loginId);
            userinDb.EmployeeID = id;
            _dbContext.Users.Update(userinDb);
            _dbContext.SaveChanges();
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





//-----------------------------------------------------------------------------------------------------------------------------

        [HttpGet("GetAllBookings")]
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _bookingsStatusRepository.GetAllBookings();
        }
        [HttpGet("GetAllPendings")]
        public async Task<IEnumerable<Booking>> GetAllPendings()
        {
            return await _bookingsStatusRepository.GetAllPendings();
        }
        [HttpGet("GetAllRejected")]
        public async Task<IEnumerable<Booking>> GetAllRejected()
        {
            return await _bookingsStatusRepository.GetAllRejected();
        }
        [HttpGet("GetAllApproved")]
        public async Task<IEnumerable<Booking>> GetAllApproved()
        {
            return await _bookingsStatusRepository.GetAllApproved();
        }
        [HttpPut("RejectBooking/{id}")]
        public async Task<IActionResult> RejectBooking(int id)
        {
            var res = await _bookingsStatusRepository.Reject(id); if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Booking with id " + id + " not available");
        }
        [HttpPut("ApproveBooking/{id}")]
        public async Task<IActionResult> ApproveBooking(int id)
        {
            var res = await _bookingsStatusRepository.Approve(id); if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Booking with id " + id + " not available");
        }
        


    }
}
