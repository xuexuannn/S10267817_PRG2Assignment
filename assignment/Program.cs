using assignment;
Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
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
        airlineDictionary[airlineCode] = airline;
    }
}
//display airline dict
foreach (var airline in airlineDictionary.Values)
{
    Console.WriteLine(airline);
}

Dictionary<string, BoardingGate> boardingGateDictionary = new Dictionary<string, BoardingGate>();
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
        boardingGateDictionary[gate] = boardingGate;
    }

}
foreach (var boardingGate in boardingGateDictionary.Values)
{
    Console.WriteLine(boardingGate);
}
}


