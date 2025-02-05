using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Markup;
//FuzzySharp was suggest by copilot as an easy way to compare strings and see if they are similar
using FuzzySharp;
using Sensor_Tower_Take_Home_Assignment.Interfaces;
/*this is a class for the DuplicateEntriesService and contains the methods and logic for finding duplicate
 * company names. For the initial implementation I prompted copilot however I have refactored the code into methods
 and to use dependency injection*/
namespace Sensor_Tower_Take_Home_Assignment.Services
{
    //Implementing interface and creating class 
    public class DuplicateEntriesService : IDuplicateEntriesService
    {
        /*This is a method that takes in a string list of names and returns a list of names that are lowercase
         and do not contain spaces for normalization. Copilot also recommended the AsParallel() method which
        "converts the list into a parallel query, allowing the subsequent operations to be processed in parallel. 
        This can improve performance for large lists by leveraging multiple CPU cores."-Copilot for optomization*/
        public List<string> NormalizeCompanyNames(List<string> companyNames)
        {
            return companyNames.AsParallel().Select(name => name.ToLower().Replace(" ", "")).ToList();
        }
        /*This method takes in a string list and an integer as parameters. Then it returns a dictionary where
         the key are a word and the values for each key are words that are similar*/
        public Dictionary<string, HashSet<string>> GroupCompanies(List<string> names, int threshold)
        {
            //updating what is happening in the console
            Console.WriteLine("Grouping company names...");
            /*creating a dictionary where the key is the name that values need to match and the values are a
             hashset of values that match the key*/
            Dictionary<string, HashSet<string>> grouped = new Dictionary<string, HashSet<string>>();
            //creating a hashset for strings called seenNames which will be used to check if a name has already been seen
            HashSet<string> seenNames = new HashSet<string>();

            //creating a variable called namesArray which is names list converted to an array from the ToArray method
            var namesArray = names.ToArray();
            //this paritioner variable was suggested by copilot to make the program faster and more optomized 
            var partitioner = Partitioner.Create(0, namesArray.Length);

            //copilot suggested implemented parralel processing for optomization
            Parallel.ForEach(partitioner, range =>
            {
                //for loop  checks if loop if seenNames contains name and if so it skips to next iteration
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var name = namesArray[i];
                    if (seenNames.Contains(name))
                    {
                        continue;
                    }
                    //creating matches list for strings
                    var matches = new List<string>();
                    //loop checks for similar names and adds them to the matches list if similar
                    for (int j = 0; j < namesArray.Length; j++)
                    {
                        /*uses fuzz ratio that we imported to check similarity and threshold which is set to 80%
                        currently but depends on what you pass in the DuplicateEntriesService constructor 
                        for threshold*/
                        if (Fuzz.Ratio(name, namesArray[j]) >= threshold && !seenNames.Contains(namesArray[j]))
                        {
                            matches.Add(namesArray[j]);
                        }
                    }
                    //If matches contains any names, the first name is used as the key
                    if (matches.Count > 0)
                    {
                        var key = matches[0];
                        /*copilot added this "Ensures thread-safe access to the grouped dictionary."
                         "Adds the matches list as a new entry in the grouped dictionary."*/
                        lock (grouped)
                        {
                            grouped[key] = new HashSet<string>(matches);
                        }
                        lock (seenNames)
                        {
                            foreach (var match in matches)
                            {
                                seenNames.Add(match);
                            }
                        }
                    }
                }
            });

           //returns dictionary grouped when method is called 
            return grouped;
        }
        /*this method takes in a dictionary called group names and displayes the company keys and their associated
        values in the console*/
        public void LogGroupedCompanies(Dictionary<string, HashSet<string>> groupedCompanies)
        {
            Console.WriteLine("Logging grouped company names...");
            Console.WriteLine("Grouped Company Names:");
           //loops through each key value pair in the dictionary
            foreach (var group in groupedCompanies)
            {
               //displays the group key
                Console.WriteLine($"\nGroup Key: {group.Key}");
                //loups through the names for each group in the hashset and displays them
                foreach (var name in group.Value)
                {
                    Console.WriteLine($"  - {name}");
                }
            }
        }
    }
}


