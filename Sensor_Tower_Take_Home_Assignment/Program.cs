using Sensor_Tower_Take_Home_Assignment.BusinessLogic;
using Sensor_Tower_Take_Home_Assignment.Interfaces;
using Sensor_Tower_Take_Home_Assignment.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
/*I prompted Copilot with the initial implementation for the problem. The Intitial Implementation was all in the 
 Program.cs and I refactored the code to follow dependency injection. I implemented dependendency injection to 
follow best practices. Some benefits of dependency injection: modularity, easier to unit test, and maintainability.
*/
class Program
{
    static void Main()
    {
        //try catch block for error handling
        try
        {
            //Next two lines I found on stack overflow for file retreival. 
            //Creating variabls to retreive the file
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string filePath = Path.Combine(dataFolderPath, "advertisers.txt");
            //making sure the file exists
            Console.WriteLine("File Exists: " + File.Exists(filePath));
            //creating a string array for each line within the file
            string[] lines = File.ReadAllLines(filePath);
            //seeing how many lines there are
            Console.WriteLine("Number of lines read: " + lines.Length);
            //converting the array to a list
            List<string> companyNames = new List<string>(lines);
            //creating the dependency
            IDuplicateEntriesService duplicateEntriesService = new DuplicateEntriesService();
            //creating and instance of the DuplicateEntries and injecting it with the dependency by passing duplicateEntriesService to the constructor
            DuplicateEntries duplicateEntries = new DuplicateEntries(companyNames, duplicateEntriesService);
        }
        //if an error occurs it will be displayed in the console
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}