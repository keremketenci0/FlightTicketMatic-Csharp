using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketMatic.Models
{
    public class Airport
    {
        public string AirportName { get; set; }
        public List<Airline> Airlines { get; set; }
    }
}
