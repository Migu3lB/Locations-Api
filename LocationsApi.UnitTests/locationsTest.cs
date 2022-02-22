using LocationsApi;
using LocationsApi.Services;
using LocationsApi.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LocationApi.UnitTests
{
    [TestClass]
    public class locationTest
    {
        private static readonly string cvsPathFile = Path.GetFullPath("Files\\Locations.csv");
        private ILocationService _locationService;

        [TestInitialize]
        public void Initalize()
        {
            _locationService = new LocationService();
        }

        [TestMethod]
        public void getAllLocations_ReturnsLocationList()
        {
            //Arrange
            List<Locations> locationList = new List<Locations>();

            //Act
            var result = _locationService.ReadCSVFile(cvsPathFile);
            locationList.AddRange(result);

            //Assert
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result != null);
            Assert.IsTrue(!string.IsNullOrEmpty(locationList.FirstOrDefault().Location));
        }

        [TestMethod]
        public void AddNewLocation_ReturnsTrue()
        {
            //Arrange
            Locations newLocation = new Locations() { 
                Id = 55,
                Location = "Theather",
                openingTime = "11:00",
                EndingTime = "21:00"
            };

            //Act
            var result = _locationService.ReadCSVFile(cvsPathFile);
            result.Add(newLocation);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Where(x => x.Location == "Theather").Count() > 0);
        }
    }
}
