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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate() { }
        public BoardingGate(string name, bool ddjb, bool cfft, bool lwtt, Flight flight)
        {
            GateName = name;
            SupportsDDJB = ddjb;
            SupportsCFFT = cfft;
            SupportsLWTT = lwtt;
            Flight = flight;
        }
        public double CalculateFees()
        {
            double baseFee = 300;
            if (SupportsCFFT == true)
            {
                baseFee += 150;
            }
            else if (SupportsDDJB == true)
            {
                baseFee += 300;
            }
            else if (SupportsLWTT == true)
            {
                baseFee += 500;
            }
            return baseFee;
        }
        public override string ToString()
        {
            return $"Gate Name: {GateName}, SupportsCFFT: {SupportsCFFT}, SupportsDDJB: {SupportsDDJB}, SupportLWTT: {SupportsLWTT}, Flight: {Flight}";
        }
    }
}
