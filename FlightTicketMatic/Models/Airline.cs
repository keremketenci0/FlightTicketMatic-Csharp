using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketMatic.Models
{
    public class Airline
    {
        public string AirlineName { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
