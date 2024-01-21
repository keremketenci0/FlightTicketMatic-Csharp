using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketMatic.Models
{
    public class Reservation
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int SeatNo { get; set; }

        public Flight flight;

        public Reservation()
        {
            flight = new Flight();
        }

        public override string ToString()
        {
            return $"Reservation Details:\n" +
                   $"First Name: {FirstName}\n" +
                   $"Last Name: {LastName}\n" +
                   $"Seat Number: {SeatNo}\n" +
                   $"Flight Details:\n" +
                   $"  Flight ID: {flight.FlightId}\n" +
                   $"  Plane No: {flight.PlaneNo}\n" +
                   $"  Date: {flight.Date}\n" +
                   $"  Time: {flight.Time}\n" +
                   $"  Capacity: {flight.Capacity}";
        }
    }
}
