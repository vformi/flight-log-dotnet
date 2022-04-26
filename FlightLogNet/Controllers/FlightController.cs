namespace FlightLogNet.Controllers
{
    using System;
    using System.Collections.Generic;

    using FlightLogNet.Facades;
    using FlightLogNet.Models;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> logger;
        private readonly FlightFacade flightFacade;

        public FlightController(ILogger<FlightController> logger, FlightFacade flightFacade)
        {
            this.logger = logger;
            this.flightFacade = flightFacade;
        }

        [HttpGet("InAir")]
        public IEnumerable<FlightModel> GetPlanesInAir()
        {
            this.logger.LogDebug("Get airplanes in Air.");
            return this.flightFacade.GetAirplanesInAir();
        }

        [HttpPost("Land")]
        public IActionResult Land(FlightLandingModel landingModel)
        {
            this.logger.LogDebug("Land flight.");
            this.flightFacade.LandFlight(landingModel);
            return this.Ok();
        }

        [HttpPost("Takeoff")]
        public IActionResult Takeoff(FlightTakeOffModel takeOffModel)
        {
            try
            {
                this.flightFacade.TakeoffFlight(takeOffModel);
                this.logger.LogDebug("Takeoff flight.");
                return this.Ok();
            }
            catch (NotSupportedException ex)
            {
                this.logger.LogError("Takeoff flight unable to proceed: " + ex);
                return this.BadRequest();
            }
        }

        [HttpGet("Report")]
        public IEnumerable<ReportModel> Report()
        {
            this.logger.LogDebug("Takeoff flight.");
            return this.flightFacade.GetReport();
        }

        [HttpGet("Export")]
        public ActionResult Export()
        {
            byte[] csv = this.flightFacade.GetExportToCsv();
            this.logger.LogDebug("Export flights into CSV.");
            return this.File(csv, "text/csv", "export.csv");
        }
    }
}
