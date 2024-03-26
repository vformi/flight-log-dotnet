namespace FlightLogNet.Repositories.Interfaces
{
    using System.Collections.Generic;

    using Models;

    public interface IAirplaneRepository
    {
        long AddGuestAirplane(AirplaneModel airplaneModel);

        bool TryGetAirplane(AirplaneModel airplaneModel, out long airplaneId);

        IList<AirplaneModel> GetClubAirplanes();
    }
}
