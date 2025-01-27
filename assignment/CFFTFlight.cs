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
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; } = 150;
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime) { }
        public override double CalculateFees()
        {
            //if (Origin == "Singapore (SIN)")
            //{
            //    double fees = 800 + 300 + RequestFee;
            //    return fees;
            //}
            //else if (Destination == "Singapore (SIN)")
            //{
            //    double fees = 500 + 300 + RequestFee;
            //    return fees;
            //}
            //else
            //{
            //    double fees = 300 + RequestFee;
            //    return fees;
            //}
            return base.CalculateFees() + RequestFee;
        }
        public override string ToString()
        {
            return base.ToString() + $"Request Fee: {RequestFee}";
        }
    }

}
