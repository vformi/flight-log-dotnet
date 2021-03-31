namespace FlightLogNet.Facades
{
    using System.Collections.Generic;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Interfaces;

    public class AirplaneFacade
    {
        private readonly IAirplaneRepository airplaneRepository;

        public AirplaneFacade(IAirplaneRepository airplaneRepository)
        {
            this.airplaneRepository = airplaneRepository;
        }

        public IEnumerable<AirplaneModel> GetClubAirplanes()
        {
            return this.airplaneRepository.GetClubAirplanes();
        }
    }
}
