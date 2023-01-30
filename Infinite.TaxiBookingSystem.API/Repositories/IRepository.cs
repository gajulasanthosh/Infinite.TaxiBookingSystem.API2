using Infinite.TaxiBookingSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public interface IGetRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int id);
        //Task<IEnumerable<Designation>> GetDesignations();
    }
    public interface IRepository<T> where T : class
    {
        Task Create(T obj);
        Task<T> Update(int id, T obj);
        Task<T> Delete(int id);
    }
    }

    public interface IEmployeeRosterRepository
    {
        Task<IEnumerable<EmployeeRoster>> SearchByEmp(string employeeName);
         
    
    }

public interface IEmployeeRepository
{
    Task<IEnumerable<Designation>> GetDesignations();
}

public interface IBookingRepository
{
    Task<IEnumerable<TaxiModel>> GetTaxiModels();
    Task<IEnumerable<TaxiType>> GetTaxiTypes();
    //Task<IEnumerable<Booking>> SearchByTaxi(string taxiModel);
}




