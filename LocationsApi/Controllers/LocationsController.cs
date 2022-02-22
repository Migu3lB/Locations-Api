using LocationsApi.Services;
using LocationsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocationsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private static readonly string cvsPathFile = Path.GetFullPath("Files\\Locations.csv");
        private readonly ILocationService _locationService;

        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILogger<LocationsController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locations>>> Get()
        {
            try
            {
                var resultData = await Task.Run(() => _locationService.ReadCSVFile(cvsPathFile));
                return resultData;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error was produced", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error getting the locations {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getbytimeranges")]
        public async Task<ActionResult<IEnumerable<Locations>>> GetBytimeRanges()
        {
            try
            {

                List<Locations> locationsFiltered = new List<Locations>();

                var openingTime = new DateTime();
                var endingTime = new DateTime();
                var openTime = DateTime.ParseExact("10:00", "H:mm", null, System.Globalization.DateTimeStyles.None);
                var endTime = DateTime.ParseExact("13:00", "H:mm", null, System.Globalization.DateTimeStyles.None);

                var resultData = await Task.Run(() => _locationService.ReadCSVFile(cvsPathFile));

                foreach (Locations location in resultData)
                {
                    DateTime.TryParseExact(location.openingTime, "H:mm", null, System.Globalization.DateTimeStyles.None, out openingTime);
                    DateTime.TryParseExact(location.EndingTime, "H:mm", null, System.Globalization.DateTimeStyles.None, out endingTime);

                    if (openingTime >= openTime && endingTime <= endTime)
                    {
                        locationsFiltered.Add(location);
                    }
                }
                return locationsFiltered;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error was produced", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error getting the locations {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Locations>> Post(Locations location)
        {
            try
            {
                if (location is null) return BadRequest();

                var newData = new Locations()
                {
                    Location = location.Location,
                    openingTime = location.openingTime,
                    EndingTime = location.EndingTime
                };

                var resultData = await Task.Run(() => _locationService.ReadCSVFile(cvsPathFile));

                newData.Id = resultData.LastOrDefault().Id + 1;

                resultData.Add(newData);

                _locationService.WriteCSVFile(cvsPathFile, resultData);

                return CreatedAtAction(nameof(Post), new { newData.Id }, newData);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error was produced", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding new location {ex.Message}");
            }
        }
    }
}
