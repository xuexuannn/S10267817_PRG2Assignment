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
    class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime)
        : base(flightNumber, origin, destination, expectedTime) // Call the base constructor
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
