using assignment;
using System.Runtime.ExceptionServices;
//==========================================================
// Student Number : S10267254
// Student Name : Andrea Lim Shi Hui
// Partner Name : Tan Xue Xuan
//==========================================================
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

Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)
    {
        string[] boardgateinfo = s.Split(',');
        string gate = boardgateinfo[0].Trim();
        bool ddjb = bool.Parse(boardgateinfo[1].Trim());
        bool cfft = bool.Parse(boardgateinfo[2].Trim());
        bool lwtt = bool.Parse(boardgateinfo[3].Trim());
        Flight flight = null;
        BoardingGate boardingGate = new BoardingGate(gate, ddjb, cfft, lwtt, flight);
        boardingGateDict[gate] = boardingGate;
    }

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
using (StreamReader sr = new StreamReader("flights.csv"))
{
    string s = sr.ReadLine();       //header
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
            flight =new NORMFlight(flightNumber,origin,destination,expectedTime);
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
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-24}{"Origin",-24}{"Destination",-24}{"Expected Departure/Arrival Time"}");
    foreach (KeyValuePair <string,Flight> kvp in flightDict)
    {
        string airlinecode = kvp.Value.FlightNumber.Substring(0, 2);    //get the first two letters
        string airlineName = "";
        foreach (KeyValuePair<string,Airline> i in airlineDict)
        {
            if (i.Key == airlinecode)
            {
                airlineName = i.Value.Name;
                break;
            }
        }
        Console.WriteLine($"{kvp.Value.FlightNumber,-16}{airlineName,-24}{kvp.Value.Origin,-24}{kvp.Value.Destination,-24}{kvp.Value.ExpectedTime}");
    }
}

//Feature 4
void DisplayGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-11}{"DDJB",-6}{"CFFT",-6}{"LWTT",-6}");
    foreach (KeyValuePair<string, BoardingGate> kvp in boardingGateDict)
    {
        Console.WriteLine($"{kvp.Value.GateName,-11}{kvp.Value.SupportsDDJB,-6}{kvp.Value.SupportsCFFT,-6}{kvp.Value.SupportsLWTT,-6}");
    }

}
//Feature 5
void AssignBoardingGate()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    Console.WriteLine("Enter Flight Number:");
    string flightNumber = Console.ReadLine();
    // Checking if flight exist
    if (!flightDict.ContainsKey(flightNumber))
    {
        Console.WriteLine("Flight not found.");
        return;
    }
    Flight selectedFlight = flightDict[flightNumber];
    // Display flight info
    Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
    Console.WriteLine($"Origin: {selectedFlight.Origin}");
    Console.WriteLine($"Destination: {selectedFlight.Destination}");
    Console.WriteLine($"Expected Time: {selectedFlight.ExpectedTime}");
    Console.Write("Special Request Code: ");
    if (selectedFlight is NORMFlight)
    {
        Console.WriteLine("None");
    }
    else if (selectedFlight is DDJBFlight)
    {
        Console.WriteLine("DDJB");
    }
    else if (selectedFlight is CFFTFlight)
    { 
        Console.WriteLine("CFFT");
    }
    else if (selectedFlight is LWTTFlight)
    {
        Console.WriteLine("LWTT");
    }
    string boardingGate;
    BoardingGate selectedBoardingGate;
    while (true)
    {
        Console.WriteLine("Enter Boarding Gate Name:");
        boardingGate = Console.ReadLine();
        if (!boardingGateDict.ContainsKey(boardingGate))
        {
            Console.WriteLine("Boarding Gate not found.");
            return;
        }
        selectedBoardingGate = boardingGateDict[boardingGate];
        if (selectedBoardingGate.Flight != null)
        {
            Console.WriteLine($"Boarding Gate {selectedBoardingGate} is already assigned to Flight {selectedBoardingGate.Flight.FlightNumber}");
            Console.WriteLine("Please choose a different Boarding Gate.");
        }
        else
        {
            break;
        }
    }
    // assign flight to the boarding gate
    selectedBoardingGate.Flight = selectedFlight;
    //update the boarding gate dict
    boardingGateDict[selectedBoardingGate.GateName] = selectedBoardingGate;

    Console.WriteLine($"Support DDJB: {selectedBoardingGate.SupportsDDJB}");
    Console.WriteLine($"Support CFFT: {selectedBoardingGate.SupportsCFFT}");
    Console.WriteLine($"Support LWTT: {selectedBoardingGate.SupportsLWTT}");
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
    string answer = Console.ReadLine().Trim().ToUpper();
    if (answer == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.WriteLine("Please select the new status of the flight: ");
        int option = Convert.ToInt32(Console.ReadLine());
        if (option == 1)
        {
            selectedFlight.Status = "Delayed";
        }
        else if (option == 2)
        {
            selectedFlight.Status = "Boarding";
        }
        else if (option == 3)
        {
            selectedFlight.Status = "On Time";
        }
        else
        {
            Console.WriteLine("Invalid option");
        }
    }
    else
    {
        selectedFlight.Status = "On Time";
    }
    Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {selectedBoardingGate.GateName}!");
}
AssignBoardingGate();


//feature 6
void CreateFlight()
{
    while (true)
    {
        Console.Write("Enter Flight Number: ");
        string number = Console.ReadLine();
        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();
        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        string stringTime = Console.ReadLine();
        try
        {
            DateTime dateTime = DateTime.ParseExact(stringTime, "dd/mm/yyyy hh:mm", null);
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string code = Console.ReadLine().ToUpper();
            Flight newFlight = null;
            if (code == "CFFT")
            {
                newFlight = new CFFTFlight(number, origin, destination, dateTime);
            }
            else if (code == "DDJB")
            {
                newFlight = new DDJBFlight(number, origin, destination, dateTime);
            }
            else if (code == "LWTT")
            {
                newFlight = new LWTTFlight(number, origin, destination, dateTime);
            }
            else if (code == "NONE")
            {
                newFlight = new NORMFlight(number, origin, destination, dateTime);
                code = "";
            }
            else
            {
                Console.WriteLine("Invalid Special Request Code.");
                continue;
            }
            if (newFlight != null)
            {
                flightDict[number] = newFlight;
                using (StreamWriter sw = new StreamWriter("flights.csv", true))
                {
                    sw.WriteLine($"{number},{origin},{destination},{dateTime:hh:mm tt},{code}");
                }
                Console.WriteLine($"Flight {number} has been added!");
            }
            Console.Write("Would you like to add another flight? (Y/N): ");
            string answer = Console.ReadLine().Trim().ToUpper();

            if (answer == "N")
            {
                break;
            }

        }
        catch (FormatException)
        {
            Console.WriteLine("The date format is invalid. Please provide the date in the format dd/mm/yyyy hh:mm");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }
    }
}

void DisplayAirlines()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15}{"Airline Name"}");
    foreach (KeyValuePair<string, Airline> kvp in airlineDict)
    {
        Console.WriteLine($"{kvp.Value.Code,-15}{kvp.Value.Name,-6}");
    }
}

//feature 7 
DisplayAirlines();
Console.Write("Enter the 2-Letter Airline Code : ");
string aircode = Console.ReadLine()?.Trim().ToUpper();

Console.WriteLine("=============================================");
Console.WriteLine($"List of FLights for {airlineDict[aircode].Name}");
Console.WriteLine("=============================================");
List<Flight> listflights = new List<Flight>();
Console.WriteLine($"{"Flight Number",-15}{"Origin",-21}{"Destination",-14}");
foreach (var flight in flightDict.Values)
{
    if (flight.FlightNumber.StartsWith(aircode))
    {
        listflights.Add(flight);
        Console.WriteLine($"{flight.FlightNumber,-15} {flight.Origin,-8}  {flight.Destination,-14}");
    }
}
Console.Write("\nEnter a Flight Number: ");
string searchNumber = Console.ReadLine()?.Trim().ToUpper();
Flight selectedFlight = listflights.Find(f => f.FlightNumber == searchNumber);
Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-21}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time"}");
Console.WriteLine($"{selectedFlight.FlightNumber,-15}{airlineDict[aircode].Name,-21}{selectedFlight.Origin,-20}{selectedFlight.Destination,-20}{selectedFlight.ExpectedTime}");

//feature 8
DisplayAirlines();
Console.Write("Enter the 2-Letter Airline Code : ");
string airlinecodes = Console.ReadLine()?.Trim().ToUpper();
Console.WriteLine("=============================================");
Console.WriteLine($"List of FLights for {airlineDict[airlinecodes].Name}");
Console.WriteLine("=============================================");
List<Flight> flightlist = new List<Flight>();
Console.WriteLine($"{"Flight Number",-15}{"Origin",-21}{"Destination",-14}");
foreach (var flight in flightDict.Values)
{
    if (flight.FlightNumber.StartsWith(airlinecodes))
    {
        flightlist.Add(flight);
        Console.WriteLine($"{flight.FlightNumber,-15} {flight.Origin,-8}  {flight.Destination,-14}");
    }
}
Console.WriteLine("Choose an existing Flight to modify or delete: ");
string editflight = Console.ReadLine()?.Trim().ToUpper();
var flightToModify = flightlist.Find(f => f.FlightNumber == editflight);
Console.WriteLine("1. Modify Flight");
Console.WriteLine("2. Delete Flight");
Console.Write("Choose an option: ");
string option = Console.ReadLine();
if (option == "1")
{
    Console.WriteLine("1.Modify Basic Information");
    Console.WriteLine("2. Modify Status");
    Console.WriteLine("3. Modify Special Request Code");
    Console.WriteLine("4. Modify Boarding Gate");
    Console.Write("Choose an option: ");
    string nextoption = Console.ReadLine();
    if (nextoption == "1")
    {
        Console.Write("Enter new Origin: ");
        string newOrigin = Console.ReadLine()?.Trim();
        flightToModify.Origin = newOrigin;
        Console.Write("Enter new Destination: ");
        string newDest = Console.ReadLine()?.Trim();
        flightToModify.Destination = newDest;
        Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        DateTime newTime = Convert.ToDateTime(Console.ReadLine);
        flightToModify.ExpectedTime = newTime;
    }
    else if (nextoption == "2")
    {
        Console.WriteLine("Enter a new status: ");
        flightToModify.Status = Console.ReadLine()?.Trim();

    }

}
else if (option == "2")
{
    // Delete Flight
    Console.Write("Enter the Flight Number to delete: ");
    string deleteflight = Console.ReadLine()?.Trim();

    var flightToDelete = flightlist.Find(f => f.FlightNumber == deleteflight);
    Console.Write($"Are you sure you want to delete {flightToDelete.FlightNumber}? [Y/N]: ");
    string confirm = Console.ReadLine()?.Trim().ToUpper();

    if (confirm == "Y")
    {
        flightDict.Remove(flightToDelete.FlightNumber);
        Console.WriteLine("Flight deleted successfully.");
    }
    else
    {
        Console.WriteLine("Deletion cancelled.");
    }
}

//feature 9
void DisplayFlightSchedule()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-24}{"Origin",-24}{"Destination",-24}{"Expected Departure/Arrival Time",-24}{"Status",-24}{"Boarding Gate"}");
    List<Flight> flightList = new List<Flight>();
    foreach (KeyValuePair<string,Flight> kvp in flightDict)
    {
        flightList.Add(kvp.Value);
    }
    flightList.Sort();
}