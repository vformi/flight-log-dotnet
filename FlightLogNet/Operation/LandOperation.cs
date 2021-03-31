namespace FlightLogNet.Operation
{
    using System;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Interfaces;

    public class LandOperation
    {
        private readonly IFlightRepository flightRepository;

        public LandOperation(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public void Execute(FlightLandingModel landingModel)
        {
            landingModel.LandingTime = GetLocalTimeByZuluTime(landingModel.LandingTime);

            this.flightRepository.LandFlight(landingModel);
        }

        private static DateTime GetLocalTimeByZuluTime(DateTime landingTime)
        {
            if (landingTime.Kind == DateTimeKind.Utc)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(landingTime, TimeZoneInfo.Local);
            }
            else
            {
                return landingTime;
            }
        }
    }
}
