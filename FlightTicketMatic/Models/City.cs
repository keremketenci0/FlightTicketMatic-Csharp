using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketMatic.Models
{
    public class City
    {
        public string CityName { get; set; }
        public List<Airport> Airports { get; set; }
    }
}
