using assignment;

Dictionary<string, Airline> airlineDict= new Dictionary<string, Airline>();
//load airline.csv
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    string? s = sr.ReadLine(); // read the heading
                               // display the heading
    while ((s = sr.ReadLine()) != null)
    {
        string[] airinfo = s.Split(',');
        string airlineName = airinfo[0].Trim();
        string airlineCode = airinfo[1].Trim();
        Airline airline = new Airline(airlineName, airlineCode);
        airlineDict[airlineCode] = airline;
    }
}
//display airline dict
foreach (var airline in airlineDict.Values)
{
    Console.WriteLine(airline);
}

Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
using(StreamReader sr = new StreamReader("boardinggates.csv"))
{
    string?s =sr.ReadLine();
    while ((s=sr.ReadLine()) != null)
    {
        string[] boardgateinfo = s.Split(',');
        string gate= boardgateinfo[0].Trim();
        bool ddjb = bool.Parse(boardgateinfo[1].Trim());
        bool cfft = bool.Parse(boardgateinfo[2].Trim());
        bool lwtt = bool.Parse(boardgateinfo[3].Trim());
        Flight flight = null;
        BoardingGate boardingGate = new BoardingGate(gate, ddjb, cfft, lwtt, flight);
        boardingGateDict[gate] = boardingGate;
    }

}
foreach (var boardingGate in boardingGateDict.Values)
{
    Console.WriteLine(boardingGate);
}

void MainMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
    Console.WriteLine("Please select your option");
}

//Feature 2
Dictionary<string,Flight> flightDict =new Dictionary<string,Flight>();
using (StreamReader sr = new StreamReader("Flight.csv"))
{
    string s = sr.ReadLine();//header
    while ((s=sr.ReadLine()) != null)
    {
        string[] parts = s.Split(',');
        string flightNumber = parts[0].Trim();
        string origin = parts[1].Trim();
        string destination = parts[2].Trim();
        DateTime expectedTime= DateTime.Parse(parts[3].Trim());
        string specialRequestCode = "";
        if (parts.Length > 4)
        {
            specialRequestCode = parts[4].Trim();
        }
        Flight flight;


        if (specialRequestCode == "DDJB")
        {
            flight = new DDJBFlight(flightNumber, origin, destination,expectedTime);
        }
        else if (specialRequestCode == "CFFT")
        {
            flight = new CFFTFlight(flightNumber,origin, destination,expectedTime);
        }
        else if (specialRequestCode == "LWTT")
        {
            flight = new LWTTFlight(flightNumber, origin, destination,expectedTime);
        }
        else
        {
            flight =new Flight(flightNumber,origin,destination,expectedTime);
        }
        if (flightDict.ContainsKey(flightNumber))
        {
            Console.WriteLine($"Duplicate flight number {flightNumber}.");
        }
        else
        {
            flightDict[flightNumber] = flight;
        }
    }
}
//Feature 3
void DisplayFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",16}{"Airline Name",24}{"Origin",24}{"Destination",24}{"Expected Departure/Arrival Time"}");
    foreach (KeyValuePair <string,Flight> kvp in flightDict)
    {
        string airlinecode = kvp.Value.FlightNumber.Substring(0, 2);    //get the first two letters
        string airlineName = "";
        foreach (KeyValuePair<string,Airline> kvp in airlineDict)
        {
            if (kvp.Key == airlinecode)
            {
                airlineName = kvp.Value.Name;
                break;
            }
        }
        Console.WriteLine($"{kvp.Value.FlightNumber,16}{airlineName,24}{kvp.Value.Origin,24}{kvp.Value.Destination,24}{kvp.Value.ExpectedTime}");
    }
}

