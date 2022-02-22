using CsvHelper;
using LocationsApi.Mappers;
using LocationsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationsApi.Services
{
    public class LocationService : ILocationService
    {
        public List<Locations> ReadCSVFile(string filepath)
        {
            List<Locations> locations = new List<Locations>();
            try
            {
                using (var reader = new StreamReader(filepath, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<LocationMap>();
                    locations = csv.GetRecords<Locations>().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return locations;
        }

        public void WriteCSVFile(string filepath, List<Locations> location)
        {
            using (StreamWriter sw = new StreamWriter(filepath, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                cw.WriteHeader<Locations>();
                cw.NextRecord();
                foreach (Locations stu in location)
                {
                    cw.WriteRecord(stu);
                    cw.NextRecord();
                }
            }
        }
    }
}
