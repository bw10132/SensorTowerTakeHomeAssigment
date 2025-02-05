using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensor_Tower_Take_Home_Assignment.Interfaces;
/*DuplicateEntries has a duplicateEntriesService dependency injected through the constructor and then calls
 methods from the duplicateEntriesService object*/

namespace Sensor_Tower_Take_Home_Assignment.BusinessLogic
{
    public class DuplicateEntries
    {
        //creating a private field of IDuplicateEntriesService interface 
        private readonly IDuplicateEntriesService _duplicateEntriesService;
        /*constructor that takes a string list of company names and a object that implements the 
         IDuplicateEntriesService interface*/
        public DuplicateEntries(List<string> companyNames, IDuplicateEntriesService duplicateEntriesService)
        {
            /*assigning duplicateEntriesService to private object
             that implements IDuplicateEntriesService interface*/
            _duplicateEntriesService = duplicateEntriesService;
            //writeline statements in console are to show what is happening in the console
            Console.WriteLine("Starting normalization...");
            /*creating variable that equals a list of Normalized names returned from the NormalizeCompanyNames method.
             companyNames list is passed as a parameter*/
            var normalizedNames = _duplicateEntriesService.NormalizeCompanyNames(companyNames);
            Console.WriteLine("Normalization complete.");
            Console.WriteLine("Starting grouping...");
            /*Creating a groupedCompanies variable that returns a dictionary where the key is the similar name and 
            the values are the names in a hashset that match that similar name key. A list is of names is passed into the
            method parameter along with an integer that represents the percentage of accuracy required 
            for matching keys to the values*/
            var groupedCompanies = _duplicateEntriesService.GroupCompanies(normalizedNames, 80);
            Console.WriteLine("Grouping complete.");
            /*When LogGroupedCompanies method is called and a data dictionary of grouped names is passed as the 
             parameter it will print out the grouped names in the console*/
            _duplicateEntriesService.LogGroupedCompanies(groupedCompanies);
        }
    }

}






