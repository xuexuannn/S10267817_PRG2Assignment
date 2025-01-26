using assignment;
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
namespace assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Airline() { }
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }
        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            Flights[flight.FlightNumber] = flight;
            return true;
        }
        public double CalculateFees()
        {
            double totalFee = 0;
            double secondDiscount = 0;
            foreach (Flight flight in Flights.Values)
            {
                totalFee += flight.CalculateFees();
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                {
                    secondDiscount += 110;
                }
                if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (Bkk)" || flight.Origin == "Tokyo (NRT)")
                {
                    secondDiscount += 25;
                }
                if (flight is NORMFlight)
                {
                    secondDiscount += 50;
                }
            }
            double discount = 0;
            if (Flights.Count > 0)
            {
                discount = totalFee * 0.03;
            }
            double threeFlightDiscount = (Flights.Count / 3) * 350;
            totalFee -= discount;
            totalFee -= secondDiscount;
            totalFee-=threeFlightDiscount;
            return totalFee;
        }
            
        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{Code,-15}{Name}";
        }
    }
}
