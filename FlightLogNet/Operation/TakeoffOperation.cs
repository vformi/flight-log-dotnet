namespace FlightLogNet.Operation
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Repositories.Interfaces;

    public class TakeoffOperation(
        IFlightRepository flightRepository,
        IAirplaneRepository airplaneRepository,
        CreatePersonOperation createPersonOperation)
    {
        private const int GuestId = 0;

        public void Execute(FlightTakeOffModel takeOffModel)
        {
            if (takeOffModel.Towplane == null)
            {
                throw new NotSupportedException("Can not takeoff flight with null towplane.");
            }

            takeOffModel.TakeoffTime = GetLocalTimeByZuluTime(takeOffModel.TakeoffTime);

            long? towplaneFlightId = this.CreateFlight(takeOffModel.Towplane, takeOffModel.TakeoffTime, takeOffModel.Task, FlightType.Towplane);
            long? gliderFlightId = this.CreateFlight(takeOffModel.Glider, takeOffModel.TakeoffTime, takeOffModel.Task, FlightType.Glider);

            flightRepository.TakeoffFlight(gliderFlightId, towplaneFlightId);
        }

        private long? CreateFlight(AirplaneWithCrewModel airplaneWithCrewModel, DateTime takeoffTime, string task, FlightType type)
        {
            if (airplaneWithCrewModel is null)
            {
                return null;
            }

            var model = new CreateFlightModel
            {
                AirplaneId = this.CreateAirplane(airplaneWithCrewModel.Airplane),
                PilotId = this.CreatePerson(airplaneWithCrewModel.Pilot) ?? throw new KeyNotFoundException("Pilot not found"),
                CopilotId = this.CreatePerson(airplaneWithCrewModel.Copilot),
                Type = type,
                TakeOffTime = takeoffTime,
                Task = task
            };

            long towplaneFlightId = flightRepository.CreateFlight(model);
            return towplaneFlightId;
        }

        private long? CreatePerson(PersonModel personModel)
        {
            return createPersonOperation.Execute(personModel);
        }

        private long CreateAirplane(AirplaneModel airplaneModel)
        {
            if (airplaneModel?.Id == GuestId)
            {
                return airplaneRepository.AddGuestAirplane(airplaneModel);
            }

            if (airplaneRepository.TryGetAirplane(airplaneModel, out long airplaneId))
            {
                return airplaneId;
            }

            throw new KeyNotFoundException("Airplane not found");
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
