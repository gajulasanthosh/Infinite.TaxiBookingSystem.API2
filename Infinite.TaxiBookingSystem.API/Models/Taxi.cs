using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Models
{
    public class Taxi
    {
        public int TaxiId { get; set; }
        //foreign
        public int TaxiTypeId { get; set; }
        public int TaxiModelId { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }

        //navigation
        public TaxiType TaxiType { get; set; }
        public TaxiModel TaxiModel { get; set; }

       


    }
}
