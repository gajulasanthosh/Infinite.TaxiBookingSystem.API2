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
    public class BookingsController : ControllerBase
    {
        private readonly IRepository<Booking> _repository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IGetRepository<BookingDto> _bookingDtoRepository;
        private readonly ApplicationDbContext _dbContext;


        public BookingsController(IRepository<Booking> repository, IBookingRepository bookingRepository, IGetRepository<BookingDto> bookingDtoRepository,ApplicationDbContext dbContext)
        {
            _repository = repository;
            _bookingRepository = bookingRepository;
            _bookingDtoRepository = bookingDtoRepository;
            _dbContext = dbContext;
        }
        [HttpGet("GetAllBookings")]
        public IEnumerable<BookingDto> GetBookings()
        {
            return _bookingDtoRepository.GetAll();
        }
        [HttpGet]
        [Route("GetBookingById/{id}", Name = "GetBookingById")]
        public async Task<ActionResult> GetBookingById(int id)
        {
            var booking = await _bookingDtoRepository.GetById(id);
            if (booking != null)
            {
                return Ok(booking);
            }
            return NotFound("Booking not found");
        }

        //[HttpGet("SearchBooking/{taxiModel}")]

        //public async Task<IActionResult> SearchBookingByTaxi(string taxiModel)
        //{
        //    var result = await _bookingRepository.SearchByTaxi(taxiModel);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Please provide valid taxi");

        //}
        [Authorize]
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            booking.BookingDate = DateTime.Now;
            int id = booking.CustomerID;
            var loginId = User.FindFirstValue(ClaimTypes.Name);
            var x = _dbContext.Users.FirstOrDefault(y => y.LoginID == loginId);
            var booking2 = _dbContext.Customers.FirstOrDefault(y => y.CustomerId == x.CustomerID);

            id =booking2.CustomerId;
            //var id = _dbContext.Customers.Where(x => x.CustomerId)
            //customerdata.CustomerID = booking.CustomerID;
            await _repository.Create(booking);

            return CreatedAtRoute("GetBookingById", new { id = booking.BookingId }, booking);

        }
        [HttpPut("UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, booking);
            if (result != null)
            {

                return NoContent();
            }
            return NotFound("Booking not found");
        }
        [HttpDelete("DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Booking not found");
        }
        [HttpGet("GetTaxiModels")]
        public async Task<IActionResult> GetTaxiModels()
        {
            var taximodels = await _bookingRepository.GetTaxiModels();
            return Ok(taximodels);
        }
        [HttpGet("GetTaxiTypes")]
        public async Task<IActionResult> GetTaxiTypes()
        {
            var taxitypes = await _bookingRepository.GetTaxiTypes();
            return Ok(taxitypes);
        }
    }
}
