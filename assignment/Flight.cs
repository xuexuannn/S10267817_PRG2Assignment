using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10267254
// Student Name : Andrea Lim Shi Hui
// Partner Name : Tan Xue Xuan
//==========================================================
namespace S10267254_PRG2Assignment
{
    abstract class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = "Scheduled";
        }

        public virtual double CalculateFees()
        {
            double fees = 300; // Base Boarding Gate Fee
            if (Origin == "Singapore (SIN)")
            {
                fees += 800;
            }
            else if (Destination == "Singapore (SIN)")
            {
                fees += 500;
            }
            return fees;
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber},Origin: {Origin},Destination: {Destination},Expected Time: {ExpectedTime.ToString("hh:mm tt")}";
        }
        public int CompareTo(Flight flight)
        {
            return ExpectedTime.CompareTo(flight.ExpectedTime);
        }
    }
}



