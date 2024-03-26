namespace FlightLogNet.Operation
{
    using System;

    using Models;
    using Repositories.Interfaces;

    public class LandOperation(IFlightRepository flightRepository)
    {
        public void Execute(FlightLandingModel landingModel)
        {
            landingModel.LandingTime = GetLocalTimeByZuluTime(landingModel.LandingTime);

            flightRepository.LandFlight(landingModel);
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
