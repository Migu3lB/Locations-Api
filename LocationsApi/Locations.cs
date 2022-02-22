using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationsApi
{
    public class Locations
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("location_name")]
        public string Location { get; set; }
        [Name("opening_time")]
        public string openingTime { get; set; }
        [Name("ending_time")]
        public string EndingTime { get; set; }
    }
}
