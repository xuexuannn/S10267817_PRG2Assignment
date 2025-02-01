using assignment;
using S10267254_PRG2Assignment;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
//==========================================================
// Student Number : S10267254
// Student Name : Andrea Lim Shi Hui
// Partner Name : Tan Xue Xuan
//==========================================================
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
    Console.WriteLine("8. Advanced Feature 1");
    Console.WriteLine("9. Advanced Feature 2");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
}

//Feature 1
Dictionary<string, Airline> airlineDict= new Dictionary<string, Airline>();
try
{
    Console.WriteLine("Loading Airlines...");
    int count = 0;
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
            count++;
        }
    }
    Console.WriteLine($"{count} Airlines Loaded!");
}
catch (FileNotFoundException)
{
    Console.WriteLine("'airlines.csv' file not found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error while reading airlines file: {ex.Message}");
}

Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
try
{
    Console.WriteLine("Loading Boarding Gates...");
    int boardCount = 0;
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
            boardCount++;
        }

    }
    Console.WriteLine($"{boardCount} Boarding Gates Loaded!");
}
catch (FileNotFoundException)
{
    Console.WriteLine("'boardinggates.csv' file not found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error while reading boarding gates file: {ex.Message}");
}

//Feature 2
Dictionary<string,Flight> flightDict =new Dictionary<string,Flight>();
try
{
    Console.WriteLine("Loading FLights...");
    int flightCount = 0;
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string s = sr.ReadLine();       //header
        while ((s = sr.ReadLine()) != null)
        {
            string[] parts = s.Split(',');
            string flightNumber = parts[0].Trim();
            string origin = parts[1].Trim();
            string destination = parts[2].Trim();
            DateTime expectedTime = DateTime.Parse(parts[3].Trim());
            string specialRequestCode = "";
            if (parts.Length > 4)
            {
                specialRequestCode = parts[4].Trim();
            }
            Flight flight;

            if (specialRequestCode == "DDJB")
            {
                flight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
            }
            else if (specialRequestCode == "CFFT")
            {
                flight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
            }
            else if (specialRequestCode == "LWTT")
            {
                flight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
            }
            else
            {
                flight = new NORMFlight(flightNumber, origin, destination, expectedTime);
            }
            if (flightDict.ContainsKey(flightNumber))
            {
                Console.WriteLine($"Duplicate flight number {flightNumber}.");
            }
            else
            {
                flightDict[flightNumber] = flight;
                flightCount++;
            }
        }
    }
    Console.WriteLine($"{flightCount} FLights Loaded!");
}
catch (FileNotFoundException)
{
    Console.WriteLine("'flights.csv' file not found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error while reading flights file: {ex.Message}");
}
//Feature 3
void DisplayFlight()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-24}{"Origin",-24}{"Destination",-24}{"Expected Departure/Arrival Time"}");
        foreach (KeyValuePair<string, Flight> kvp in flightDict)
        {
            string airlinecode = kvp.Value.FlightNumber.Substring(0, 2);    //get the first two letters
            string airlineName = "";
            foreach (KeyValuePair<string, Airline> i in airlineDict)
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
    catch (Exception ex)
    {
        Console.WriteLine($"Error Message: {ex.Message}");
    }
}

//Feature 4
void DisplayGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-11}{"DDJB",-10}{"CFFT",-10}{"LWTT",-10}");
    foreach (KeyValuePair<string, BoardingGate> kvp in boardingGateDict)
    {
        Console.WriteLine($"{kvp.Value.GateName,-11}{kvp.Value.SupportsDDJB,-10}{kvp.Value.SupportsCFFT,-10}{kvp.Value.SupportsLWTT,-10}");
    }

}
//Feature 5
void AssignBoardingGate()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");

        Console.WriteLine("Enter Flight Number:");
        string flightNumber = Console.ReadLine().ToUpper();
        // Checking if flight exist
        if (!flightDict.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found.");
            return;
        }
        Flight selectedFlight = flightDict[flightNumber];
        string boardingGate;
        BoardingGate selectedBoardingGate;
        while (true)
        {
            Console.WriteLine("Enter Boarding Gate Name:");
            boardingGate = Console.ReadLine().ToUpper();
            if (!boardingGateDict.ContainsKey(boardingGate))
            {
                Console.WriteLine("Boarding Gate not found.");
            }
            else
            {
                selectedBoardingGate = boardingGateDict[boardingGate];
                if (selectedBoardingGate.Flight != null)
                {
                    Console.WriteLine($"Boarding Gate {selectedBoardingGate.GateName} is already assigned to Flight {selectedBoardingGate.Flight.FlightNumber}");
                    Console.WriteLine("Please choose a different Boarding Gate.");
                }
                else
                {
                    break;
                }
            }
        }
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
        Console.WriteLine($"Boarding Gate Name: {selectedBoardingGate.GateName}");
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
            while (true)
            {
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                Console.WriteLine("Please select the new status of the flight: ");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    selectedFlight.Status = "Delayed";
                    break;
                }
                else if (option == 2)
                {
                    selectedFlight.Status = "Boarding";
                    break;
                }
                else if (option == 3)
                {
                    selectedFlight.Status = "On Time";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again");
                    continue;
                }
            }
        }
        else
        {
            selectedFlight.Status = "On Time";
        }
        Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {selectedBoardingGate.GateName}!");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Invalid input format. Please enter the correct data. Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}

//feature 6
void CreateFlight()
{
    while (true)
    {
        string number;
        while (true)
        {
            Console.Write("Enter Flight Number: ");
            number = Console.ReadLine().ToUpper();
            if (flightDict.ContainsKey(number))
            {
                Console.WriteLine("Flight Number is already exist. Please try again");
                continue;
            }
            if (number.Length > 2)
            {
                string airlineCode = number.Substring(0, 2);
                if (!airlineDict.ContainsKey(airlineCode))
                {
                    Console.WriteLine($"Airline {airlineCode} does not exist. Please try again");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid Flight Number. Please try again.");
                continue;
            }
            break;
        }
        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();
        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        string stringTime = Console.ReadLine();
        try
        {
            string[] formats = { "d/M/yyyy HH:mm", "dd/MM/yyyy HH:mm", "d/M/yyyy hh:mm tt", "dd/MM/yyyy hh:mm tt" };
            DateTime dateTime = DateTime.ParseExact(stringTime, formats, null);
            Flight newFlight = null;
            string code = "";
            while (true)
            {
                Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                code = Console.ReadLine().ToUpper();
                if (code == "CFFT")
                {
                    newFlight = new CFFTFlight(number, origin, destination, dateTime);
                    break;
                }
                else if (code == "DDJB")
                {
                    newFlight = new DDJBFlight(number, origin, destination, dateTime);
                    break;
                }
                else if (code == "LWTT")
                {
                    newFlight = new LWTTFlight(number, origin, destination, dateTime);
                    break;
                }
                else if (code == "NONE")
                {
                    newFlight = new NORMFlight(number, origin, destination, dateTime);
                    code = "";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Special Request Code. Please try again.");
                    continue;
                }
            }
            if (newFlight != null)
            {
                flightDict[number] = newFlight;
                using (StreamWriter sw = new StreamWriter("flights.csv", true))
                {
                    sw.WriteLine($"{number},{origin},{destination},{dateTime.ToString("dd/MM/yyyy hh:mm tt")},{code}");
                }
                Console.WriteLine($"Flight {number} has been added!");
            }
            Console.WriteLine("Would you like to add another flight? (Y/N): ");
            string answer = Console.ReadLine().Trim().ToUpper();

            if (answer == "N")
            {
                break;
            }

        }
        catch (FormatException)
        {
            Console.WriteLine("The date format is invalid. Please provide the date in the format dd/mm/yyyy hh:mm. Please re enter the Flight Information.");
            continue;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: flights.csv file not found. Please check the file and try again.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: Unable to write to the file. {ex.Message}");
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
void DisplayDetails()
{
    try
    {
        DisplayAirlines();
        Console.Write("Enter the 2-Letter Airline Code : ");
        string aircode = Console.ReadLine()?.Trim().ToUpper();
        if (string.IsNullOrEmpty(aircode) || !airlineDict.ContainsKey(aircode))
        {
            Console.WriteLine("Please enter a valid 2-letter code from the list.");
        }
        Console.WriteLine("=============================================");
        Console.WriteLine($"List of FLights for {airlineDict[aircode].Name}");
        Console.WriteLine("=============================================");
        List<Flight> listflights = new List<Flight>();
        Console.WriteLine($"{"Flight Number",-15}{"Origin",-20}{"Destination"}");
        foreach (var flight in flightDict.Values)
        {
            if (flight.FlightNumber.StartsWith(aircode))
            {
                listflights.Add(flight);
                Console.WriteLine($"{flight.FlightNumber,-14} {flight.Origin,-18}  {flight.Destination}");
            }
        }
        Console.Write("\nEnter a Flight Number: ");
        string searchNumber = Console.ReadLine()?.Trim().ToUpper();
        Flight selectedFlight = listflights.Find(f => f.FlightNumber == searchNumber);
        Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-21}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time"}");
        Console.WriteLine($"{selectedFlight.FlightNumber,-15}{airlineDict[aircode].Name,-21}{selectedFlight.Origin,-20}{selectedFlight.Destination,-20}{selectedFlight.ExpectedTime}");
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine(" The airline code entered does not exist.");
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input format. Please enter correct values.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" An unexpected error occurred: {ex.Message}");
    }
}
//DisplayDetails();

//feature 8
void ModifyFlight()
{
    try
    {
        DisplayAirlines();
        string? newGate = null;
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
        if (flightDict.ContainsKey(editflight))


        {
            var flightToModify = flightDict[editflight]; // Access directly since key exists
            Console.WriteLine("1. Modify Flight");
            Console.WriteLine("2. Delete Flight");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();
            if (option == "1")
            {
                Console.WriteLine("1. Modify Basic Information");
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
                    DateTime newTime = DateTime.Parse(Console.ReadLine()?.Trim()!);
                    flightToModify.ExpectedTime = newTime;
                }
                else if (nextoption == "2")
                {
                    Console.Write("Enter a new status: ");
                    flightToModify.Status = Console.ReadLine()?.Trim();

                }
                else if (nextoption == "3")
                {
                    Console.Write("Enter new special request code: ");
                    string newCode = Console.ReadLine()?.Trim();
                    if (newCode == "DDJB")
                    {
                        flightToModify = new DDJBFlight(flightToModify.FlightNumber, flightToModify.Origin, flightToModify.Destination, flightToModify.ExpectedTime);
                    }
                    else if (newCode == "CFFT")
                    {
                        flightToModify = new CFFTFlight(flightToModify.FlightNumber, flightToModify.Origin, flightToModify.Destination, flightToModify.ExpectedTime);
                    }
                    else if (newCode == "LWTT")
                    {
                        flightToModify = new LWTTFlight(flightToModify.FlightNumber, flightToModify.Origin, flightToModify.Destination, flightToModify.ExpectedTime);
                    }
                    else
                    {
                        flightToModify = new NORMFlight(flightToModify.FlightNumber, flightToModify.Origin, flightToModify.Destination, flightToModify.ExpectedTime);
                    }
                    flightDict[flightToModify.FlightNumber] = flightToModify; // Update in the dictionary

                }
                else if (nextoption == "4")
                {
                    Console.Write("Enter new boarding gate: ");
                    newGate = Console.ReadLine()?.Trim();
                }
                // Reflect changes in the dictionary
                flightDict[flightToModify.FlightNumber] = flightToModify;
                // Display updated flight details
                Console.WriteLine("\nModified Flight Details:");
                Console.WriteLine($"Flight Number: {flightToModify.FlightNumber}");
                Console.WriteLine($"Origin: {flightToModify.Origin}");
                Console.WriteLine($"Destination: {flightToModify.Destination}");
                Console.WriteLine($"Expected Time: {flightToModify.ExpectedTime}");
                Console.WriteLine($"Status: {flightToModify.Status}");
                Console.WriteLine($"Special Request Code: {flightToModify.GetType().Name}");
                if (string.IsNullOrEmpty(newGate) || !boardingGateDict.ContainsKey(newGate))
                {
                    Console.WriteLine("Boarding Gate: Unassigned");
                }
                else
                {
                    BoardingGate modifiedGate = boardingGateDict[newGate];
                    Console.WriteLine($"Boarding Gate: {modifiedGate.GateName}");
                }
            }


            else if (option == "2")
            {
                // Delete Flight
                Console.Write("Enter the Flight Number to delete: ");
                string deleteflight = Console.ReadLine()?.Trim();


                Console.Write($"Are you sure you want to delete {editflight}? [Y/N]: ");
                string confirm = Console.ReadLine()?.Trim().ToUpper();

                if (confirm == "Y")
                {
                    flightDict.Remove(editflight);
                    Console.WriteLine("Flight deleted successfully.");
                }
                else if (confirm == "N")
                {
                    Console.WriteLine("Deletion cancelled.");
                }
                else
                {
                    Console.WriteLine("Invalid Input. ");
                }
            }
        }
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine("The airline code or flight number entered does not exist.");
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input format. Please enter correct values.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" An unexpected error occurred: {ex.Message}");
    }
}
//feature 9
void DisplayFlightSchedule()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-24}{"Origin",-24}{"Destination",-24}{"Expected Departure/Arrival Time",-35}{"Status",-24}{"Boarding Gate",-24}{"Special Request"}");
        List<Flight> flightList = new List<Flight>();
        foreach (KeyValuePair<string, Flight> kvp in flightDict)
        {
            flightList.Add(kvp.Value);
        }
        flightList.Sort();
        foreach (Flight flight in flightList)
        {
            string boardinggate = "Unassigned";
            string specialRequest = "None";
            if (flight is DDJBFlight)
            {
                specialRequest = "DDJB";
            }
            else if (flight is CFFTFlight)
            {
                specialRequest = "CFFT";
            }
            else if (flight is LWTTFlight)
            {
                specialRequest = "LWWT";
            }
            foreach (KeyValuePair<string, BoardingGate> kvp in boardingGateDict)
            {
                if (kvp.Value.Flight != null && kvp.Value.Flight.FlightNumber == flight.FlightNumber)
                {
                    boardinggate = kvp.Key;
                    break;
                }
            }
            string airlinecode = flight.FlightNumber.Substring(0, 2);    //get the first two letters
            string airlineName = "";
            foreach (KeyValuePair<string, Airline> i in airlineDict)
            {
                if (i.Key == airlinecode)
                {
                    airlineName = i.Value.Name;
                    break;
                }
            }
            try
            {
                Console.WriteLine($"{flight.FlightNumber,-16}{airlineName,-24}{flight.Origin,-24}{flight.Destination,-24}{flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt"),-35}{flight.Status,-24}{boardinggate,-24}{specialRequest}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error formatting flight time for flight {flight.FlightNumber}. {ex.Message}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}

//Advanced feature 1
void ProcessUnassignedFlights()
{
    try
    {
        Queue<Flight> unassignedFlights = new Queue<Flight>();
        int totalUnassignedFlights = 0;
        int totalUnassignedGates = 0;
        //add and count unassigned flights
        foreach (Flight flight in flightDict.Values)
        {
            bool assigned = false;
            foreach (BoardingGate gate in boardingGateDict.Values)
            {
                if (gate.Flight != null && gate.Flight.FlightNumber == flight.FlightNumber)
                {
                    assigned = true;
                    break;
                }
            }
            if (assigned == false)
            {
                unassignedFlights.Enqueue(flight);
                totalUnassignedFlights++;
            }
        }
        //counting unassigned boarding gate
        foreach (BoardingGate gate in boardingGateDict.Values)
        {
            if (gate.Flight == null)
            {
                totalUnassignedGates++;
            }
        }
        Console.WriteLine($"Total unassigned flights: {totalUnassignedFlights}");
        Console.WriteLine($"Total unassigned boarding gates: {totalUnassignedGates}");
        int assignedFlight = 0;
        int assignedGateCount = 0;
        while (unassignedFlights.Count > 0)
        {
            Flight flights = unassignedFlights.Dequeue();
            BoardingGate assignedGate = null;
            string specialRequestCode = "None";
            if (flights is DDJBFlight)
            {
                specialRequestCode = "DDJB";
            }
            else if (flights is CFFTFlight)
            {
                specialRequestCode = "CFFT";
            }
            else if (flights is LWTTFlight)
            {
                specialRequestCode = "LWTT";
            }
            try
            {
                foreach (BoardingGate gate in boardingGateDict.Values)
                {
                    if (gate.Flight == null)
                    {
                        if (specialRequestCode == "DDJB" && gate.SupportsDDJB == true)
                        {
                            assignedGate = gate;
                            break;
                        }
                        else if (specialRequestCode == "CFFT" && gate.SupportsCFFT == true)
                        {
                            assignedGate = gate;
                            break;
                        }
                        else if (specialRequestCode == "LWTT" && gate.SupportsLWTT == true)
                        {
                            assignedGate = gate;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while assigning gate to flight {flights.FlightNumber}: {ex.Message}");
                continue;
            }
            //gate with no special code
            if (assignedGate == null)
            {
                foreach (BoardingGate gate in boardingGateDict.Values)
                {
                    if (gate.Flight == null)        // checking of gate available
                    {
                        assignedGate = gate;
                        break;
                    }
                }
            }
            //assigning the flight to gates
            if (assignedGate != null)
            {
                assignedGate.Flight = flights;
                flights.Status = "On Time";
                assignedFlight++;
                assignedGateCount++;
                Console.WriteLine($"Flight {flights.FlightNumber} assigned to Gate {assignedGate.GateName,-5} | Special Request: {specialRequestCode}");
            }
            else
            {
                Console.WriteLine($"No available gate for Flight {flights.FlightNumber}");
            }
        }
        Console.WriteLine($"Total flights processed and assigned: {assignedFlight}");
        Console.WriteLine($"Total gates processed and assigned: {assignedGateCount}");
        double flightAssignRate = 0;
        if (totalUnassignedFlights > 0)
        {
            flightAssignRate = ((double)assignedFlight / totalUnassignedFlights) * 100;
        }

        double gateAssignRate = 0;
        if (totalUnassignedGates > 0)
        {
            gateAssignRate = ((double)assignedGateCount / totalUnassignedGates) * 100;
        }
        Console.WriteLine(assignedGateCount);
        Console.WriteLine($"Percentage of flights automatically processed: {flightAssignRate:F2}%");
        Console.WriteLine($"Percentage of gates automatically processed: {gateAssignRate:F2}%");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred while processing unassigned flights: {ex.Message}");
    }
}

//Advanced Feature 2
void CheckGatesAssigned()
{
    var unassignedFlights = boardingGateDict.Values.Where(g => g.Flight == null).ToList();
    if (unassignedFlights.Count > 0)
    {
        Console.WriteLine("The following boarding gates have no assigned flights:");
        foreach (var gate in unassignedFlights)
        {
            Console.WriteLine($"Gate: {gate.GateName}");
        }
        Console.WriteLine("Please ensure all flights are assigned before proceeding.");
        return;
    }
}
void CalculateTotalFee()
{

    double totalAirlineFees = 0;
    double totalAirlineDiscounts = 0;

    foreach (var airline in airlineDict.Values)
    {
        Console.WriteLine($"Airline: {airline.Name} ({airline.Code})");

        double airlineFees = 0;
        double airlineDiscounts = 0;
        

        // Retrieve flights for this airline
        var airlineFlights = flightDict.Values.Where(f => f.FlightNumber.StartsWith(airline.Code));

        foreach (var flight in airlineFlights)
        {
            //calculate total fees needed for each airlines
            
            airlineFees += flight.CalculateFees();
          

           


            // Discounts based on promotional conditions
            if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
            {
                airlineDiscounts += 110;
                
            }
            if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)")
            {
                airlineDiscounts += 25;
                
            }
            if (flight is NORMFlight)
            {
                airlineDiscounts += 50;
                
            }
        }

        // Additional airline-level discounts
        int flightCount = airlineFlights.Count();
        if (flightCount >= 3)
        {
            airlineDiscounts += (flightCount / 3) * 350;
           
        }
        if (flightCount > 5)
        {
            airlineDiscounts += airlineFees * 0.03;
            
        }

        double finalFees = airlineFees - airlineDiscounts;

        Console.WriteLine($"  Original Subtotal : ${airlineFees:F2}");
        Console.WriteLine($"  Subtotal of Discounts: ${airlineDiscounts:F2}");
        Console.WriteLine($"  Total Final Fee: ${finalFees:F2}\n");

        totalAirlineFees += airlineFees;
        totalAirlineDiscounts += airlineDiscounts;
    }

    double finalTotalFees = totalAirlineFees - totalAirlineDiscounts;

    Console.WriteLine("=============================================");
    Console.WriteLine($"Subtotal of All Airlines: ${totalAirlineFees:F2}");
    Console.WriteLine($"Subtotal Discounts of All Airlines: ${totalAirlineDiscounts:F2}");
    Console.WriteLine($"Final Total Fees Collected: ${finalTotalFees:F2}");
    Console.WriteLine($"Percentage of Discount: {((totalAirlineDiscounts / totalAirlineFees) * 100):F2}%");

}


//Main Program Loop
while (true)
{
    try
    {
        MainMenu();
        Console.WriteLine("Please select your option");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
        {
            DisplayFlight();
        }
        else if (choice == 2)
        {
            DisplayGates();
        }
        else if (choice == 3)
        {
            AssignBoardingGate();
        }
        else if (choice == 4)
        {
            CreateFlight();
        }
        else if (choice == 5)
        {
            DisplayDetails();
        }
        else if (choice == 6)
        {
            ModifyFlight();
        }
        else if (choice == 7)
        {
            DisplayFlightSchedule();
        }
        else if (choice==8)
        {
            ProcessUnassignedFlights();
        }
        else if (choice==9)
        {
            CheckGatesAssigned();
            CalculateTotalFee();    
        }
        else if (choice == 0)
        {
            Console.WriteLine("Bye-Bye!");
            break;
        }
        else
        {
            Console.WriteLine("Invalid option. Try again! ");
            continue;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}
