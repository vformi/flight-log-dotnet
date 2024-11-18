using System.Collections.Generic;
using FlightLogNet.Models;
using FlightLogNet.Facades;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightLogNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class AirplaneController(ILogger<AirplaneController> logger, AirplaneFacade airplaneFacade) : ControllerBase
    {
        // GET method to retrieve the list of club airplanes
        [HttpGet]
        public IEnumerable<AirplaneModel> Get()
        {
            logger.LogDebug("Fetching list of club airplanes.");
            return airplaneFacade.GetClubAirplanes();
        }
    }
}
