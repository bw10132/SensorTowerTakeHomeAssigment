using Sensor_Tower_Take_Home_Assignment.BusinessLogic;
using Sensor_Tower_Take_Home_Assignment.Interfaces;
using Sensor_Tower_Take_Home_Assignment.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace SensorTowerTakeHomeAssignment.Test
{
    //Unit test giving dummy data to test the DuplicateEntries logic
    public class DuplicateEntriesTest
    {
        [Fact]
        public void DuplicateEntriesServiceTest()
        {
            //Creating a String list with dummy data
            // Arrange
            List<string> companyNames = new List<string>
        {
            "Acme Corp", "AcmeCorp", "ACME corp", "Globex Corporation", "Globex Corp", "Wayne Enterprises", "WayneEnterprise"
        };
            //creating an object called duplicateEntries which is an instance od DuplicateEntries class
            IDuplicateEntriesService duplicateEntriesService = new DuplicateEntriesService();
            
            // Act
            //using dependency injection and passing in the companyNames (which contains dummy data) as a parameter 
            DuplicateEntries duplicateEntries = new DuplicateEntries(companyNames, duplicateEntriesService);

            // Assert
            /*Copilot made the assertion statement which makes sure that for the dictionary
            the key value pairs have the correct similar values with them*/
            var normalizedNames = duplicateEntriesService.NormalizeCompanyNames(companyNames);
            var expectedNormalizedNames = new List<string> { "acmecorp", "acmecorp", "acmecorp", "globexcorporation", "globexcorp", "wayneenterprises", "wayneenterprise" };
            Assert.Equal(expectedNormalizedNames, normalizedNames);

            var groupedCompanies = duplicateEntriesService.GroupCompanies(normalizedNames, 80);
            var expectedGroupedCompanies = new Dictionary<string, HashSet<string>>
        {
            { "acmecorp", new HashSet<string> { "acmecorp", "acmecorp", "acmecorp" } },
            { "globexcorp", new HashSet<string> { "globexcorp" } },
            { "wayneenterprises", new HashSet<string> { "wayneenterprises", "wayneenterprise" } },
            { "globexcorporation", new HashSet<string> { "globexcorporation" } }
        };
            Assert.Equal(expectedGroupedCompanies, groupedCompanies);
        }
    }
}
