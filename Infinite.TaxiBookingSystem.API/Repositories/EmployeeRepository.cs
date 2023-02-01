using Infinite.TaxiBookingSystem.API.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class EmployeeRepository : IRepository<Employee>, IGetRepository<Employee>, IEmployeeRepository, IBookingsStatusRepository<Booking>
    {
        private readonly ApplicationDbContext _Context;


        public EmployeeRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Create(Employee obj)
        {
            if (obj != null)
            {
                _Context.Employees.Add(obj);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<Employee> Delete(int id)
        {
            var employeeDb = await _Context.Employees.FindAsync(id);
            if (employeeDb != null)
            {
                _Context.Employees.Remove(employeeDb);
                await _Context.SaveChangesAsync();
                return employeeDb;
            }
            return null;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _Context.Employees.ToList();
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await _Context.Employees.FindAsync(id);
            if (employee != null)
            {
                return employee;
            }
            return null;
        }



        public async Task<Employee> Update(int id, Employee obj)
        {
            var employeeDb = await _Context.Employees.FindAsync(id);
            if (employeeDb != null)
            {
                employeeDb.EmployeeName = obj.EmployeeName;
                employeeDb.Designation = obj.Designation;
                employeeDb.PhoneNo = obj.PhoneNo;
                employeeDb.EmailId = obj.EmailId;
                employeeDb.Address = obj.Address;
                //employeeDb.DrivingLicenseNo = obj.DrivingLicenseNo;
                _Context.Employees.Update(employeeDb);
                await _Context.SaveChangesAsync();
                return employeeDb;
            }
            return null;
        }

        //get designation
        public async Task<IEnumerable<Designation>> GetDesignations()
        {
            var designations = await _Context.Designations.ToListAsync();
            return designations;
        }



        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        //Bookings Status

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            var pendings = await _Context.Bookings.Where(h => h.Status == null || h.Status == "REJECTED" || h.Status == "APPROVED").ToListAsync();
            return pendings;
        }
        public async Task<IEnumerable<Booking>> GetAllPendings()
        {
            var pendings = await _Context.Bookings.Where(h => h.Status == null).ToListAsync();
            return pendings;
        }
        public async Task<IEnumerable<Booking>> GetAllRejected()
        {
            var pendings = await _Context.Bookings.Where(h => h.Status == "REJECTED").ToListAsync();
            return pendings;
        }
        public async Task<IEnumerable<Booking>> GetAllApproved()
        {
            var pendings = await _Context.Bookings.Where(h => h.Status == "APPROVED").ToListAsync();
            return pendings;
        }
        public async Task<Booking> Reject(int id)
        {
            var rejectProperty = await _Context.Bookings.FindAsync(id);
            if (rejectProperty != null)
            {
                rejectProperty.Status = "REJECTED"; _Context.Bookings.Update(rejectProperty);
                await _Context.SaveChangesAsync(); return rejectProperty;
            }
            return null;
        }
        public async Task<Booking> Approve(int id)
        {
            var rejectProperty = await _Context.Bookings.FindAsync(id);
            if (rejectProperty != null)
            {
                rejectProperty.Status = "APPROVED"; _Context.Bookings.Update(rejectProperty);
                await _Context.SaveChangesAsync(); return rejectProperty;
            }
            return null;
        }

    }


}

