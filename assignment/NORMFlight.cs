using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        : base(flightNumber, origin, destination, expectedTime, status) // Call the base constructor
        {
        }
        public override double CalculateFees()
        {
            if (Origin == "Singapore (SIN)")
            {
                double fees = 800 + 300;
                return fees;
            }
            else if (Destination == "Singapore (SIN)")
            {
                double fees = 500 + 300;
                return fees;
            }
            else
            {
                double fees = 300;
                return fees;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
