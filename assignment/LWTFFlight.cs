using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    class LWTTFlight : Flight
    {
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestfee) : base(flightNumber, origin, destination, expectedTime, status)
        { RequestFee = requestfee; }
        public double RequestFee { get; set; } = 500;

        public override double CalculateFees()
        {
            if (Origin == "Singapore (SIN)")
            {
                double fees = 800 + 300 + RequestFee;
                return fees;
            }
            else if (Destination == "Singapore (SIN)")
            {
                double fees = 500 + 300 + RequestFee;
                return fees;
            }
            else
            {
                double fees = 300 + RequestFee;
                return fees;
            }
        }
    }
}
