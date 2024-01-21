using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketMatic.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public List<City> Cities { get; set; }
    }
}
