using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationsApi.Services.Interfaces
{
    public interface ILocationService
    {
        List<Locations> ReadCSVFile(string filepath);
        void WriteCSVFile(string filepath, List<Locations> location);
    }
}
