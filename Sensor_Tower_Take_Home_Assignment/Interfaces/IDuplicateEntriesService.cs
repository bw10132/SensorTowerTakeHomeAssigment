using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Markup;
/*interface that when implemented to a class forces them to have the following methods:
NormalizeCompanyNames(List<string> companyNames), GroupCompanies(List<string> names, int threshold), 
LogGroupedCompanies(Dictionary<string, HashSet<string>> groupedCompanies)*/
namespace Sensor_Tower_Take_Home_Assignment.Interfaces
{
    public interface IDuplicateEntriesService
    {
        List<string> NormalizeCompanyNames(List<string> companyNames);
        Dictionary<string, HashSet<string>> GroupCompanies(List<string> names, int threshold);
        void LogGroupedCompanies(Dictionary<string, HashSet<string>> groupedCompanies);
    }
}
