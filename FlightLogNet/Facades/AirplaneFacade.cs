namespace FlightLogNet.Facades
{
    using System.Collections.Generic;

    using Models;
    using Repositories.Interfaces;

    public class AirplaneFacade(IAirplaneRepository airplaneRepository)
    {
        public IEnumerable<AirplaneModel> GetClubAirplanes()
        {
            return airplaneRepository.GetClubAirplanes();
        }
    }
}
