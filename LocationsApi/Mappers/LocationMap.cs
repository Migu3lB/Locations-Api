using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationsApi.Mappers
{
    public class LocationMap : ClassMap<Locations>
    {
        public LocationMap()
        {
            Map(x => x.Id).Name("id");
            Map(x => x.Location).Name("location_name");
            Map(x => x.openingTime).Name("opening_time");
            Map(x => x.EndingTime).Name("ending_time");
        }
    }
}
