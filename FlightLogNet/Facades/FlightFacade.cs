namespace FlightLogNet.Facades
{
    using System.Collections.Generic;

    using FlightLogNet.Models;
    using FlightLogNet.Operation;
    using FlightLogNet.Repositories.Interfaces;

    public class FlightFacade
    {
        private readonly IFlightRepository flightRepository;
        private readonly TakeoffOperation takeoffOperation;
        private readonly GetExportToCsvOperation getExportToCsvOperation;
        private readonly LandOperation landOperation;

        public FlightFacade(IFlightRepository flightRepository,
            TakeoffOperation takeoffOperation,
            GetExportToCsvOperation getExportToCsvOperation,
            LandOperation landOperation)
        {
            this.flightRepository = flightRepository;
            this.takeoffOperation = takeoffOperation;
            this.getExportToCsvOperation = getExportToCsvOperation;
            this.landOperation = landOperation;
        }

        internal IEnumerable<FlightModel> GetAirplanesInAir()
        {
            // TODO 2.5: Doplňte metodu repozitáře, která vrátí letadla ve vzduchu v listě modelů
            return new FlightModel[] { };
        }

        internal byte[] GetExportToCsv()
        {
            return this.getExportToCsvOperation.Execute();
        }

        internal void LandFlight(FlightLandingModel landingModel)
        {
            this.landOperation.Execute(landingModel);
        }

        internal IEnumerable<ReportModel> GetReport()
        {
            return this.flightRepository.GetReport();
        }

        internal void TakeoffFlight(FlightTakeOffModel takeOffModel)
        {
            this.takeoffOperation.Execute(takeOffModel);
        }
    }
}
