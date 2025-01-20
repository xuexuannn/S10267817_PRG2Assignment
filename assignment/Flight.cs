using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return base.ToString();
        }
    }


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
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; } = 150;
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestfee) : base(flightNumber, origin, destination, expectedTime, status)
        { RequestFee = requestfee; }
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
        public override string ToString() { return base.ToString(); }
    }
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
    class DDJBFlight : Flight
        {
            public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestfee) : base(flightNumber, origin, destination, expectedTime, status)
            { RequestFee = requestfee; }
            public double RequestFee { get; set; } = 300;

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


