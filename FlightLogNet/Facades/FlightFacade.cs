namespace FlightLogNet.Facades
{
    using System.Collections.Generic;

    using Models;
    using Operation;
    using Repositories.Interfaces;

    public class FlightFacade(
        IFlightRepository flightRepository,
        TakeoffOperation takeoffOperation,
        GetExportToCsvOperation getExportToCsvOperation,
        LandOperation landOperation)
    {
        internal IEnumerable<FlightModel> GetAirplanesInAir()
        {
            return flightRepository.GetAirplanesInAir();
        }

        internal byte[] GetExportToCsv()
        {
            return getExportToCsvOperation.Execute();
        }

        internal void LandFlight(FlightLandingModel landingModel)
        {
            landOperation.Execute(landingModel);
        }

        internal IEnumerable<ReportModel> GetReport()
        {
            return flightRepository.GetReport();
        }

        internal void TakeoffFlight(FlightTakeOffModel takeOffModel)
        {
            takeoffOperation.Execute(takeOffModel);
        }
    }
}
