using S10267254_PRG2Assignment;
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
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();
        public Terminal() { }
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }
        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code))
            {
                return false;
            }
            Airlines[airline.Code] = airline;
            return true;
        }
        public bool AddBoardingGate(BoardingGate boardGate)
        {
            if (BoardingGates.ContainsKey(boardGate.GateName))
            {
                return false;
            }
            BoardingGates[boardGate.GateName] = boardGate;
            return true;
        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (Airline airline in Airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                {
                    return airline;
                }
            }
            return null;
        }
        public void PrintAirlineFees()
        {
            foreach (Airline airline in Airlines.Values)
            {
                Console.WriteLine($"{airline.Code}: ${airline.CalculateFees()}");
            }
        }
        public override string ToString()
        {
            return $"Terminal Name: {TerminalName}";
        }
    }
}
