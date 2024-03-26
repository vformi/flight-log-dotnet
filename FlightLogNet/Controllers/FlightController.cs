namespace FlightLogNet.Controllers
{
    using System;
    using System.Collections.Generic;

    using Facades;
    using Models;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class FlightController(ILogger<FlightController> logger, FlightFacade flightFacade)
        : ControllerBase
    {
        [HttpGet("InAir")]
        public IEnumerable<FlightModel> GetPlanesInAir()
        {
            logger.LogDebug("Get airplanes in Air.");
            return flightFacade.GetAirplanesInAir();
        }

        [HttpPost("Land")]
        public IActionResult Land(FlightLandingModel landingModel)
        {
            logger.LogDebug("Land flight.");
            flightFacade.LandFlight(landingModel);
            return this.Ok();
        }

        [HttpPost("Takeoff")]
        public IActionResult Takeoff(FlightTakeOffModel takeOffModel)
        {
            try
            {
                flightFacade.TakeoffFlight(takeOffModel);
                logger.LogDebug("Takeoff flight.");
                return this.Ok();
            }
            catch (NotSupportedException ex)
            {
                logger.LogError("Takeoff flight unable to proceed: {exception}", ex);
                return this.BadRequest();
            }
        }

        [HttpGet("Report")]
        public IEnumerable<ReportModel> Report()
        {
            logger.LogDebug("Takeoff flight.");
            return flightFacade.GetReport();
        }

        [HttpGet("Export")]
        public ActionResult Export()
        {
            byte[] csv = flightFacade.GetExportToCsv();
            logger.LogDebug("Export flights into CSV.");
            return this.File(csv, "text/csv", "export.csv");
        }
    }
}
