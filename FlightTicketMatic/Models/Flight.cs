using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketMatic.Models
{
    public class Flight
    {
        public string FlightId { get; set; }
        public string PlaneNo { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Capacity { get; set; }
    }
}
