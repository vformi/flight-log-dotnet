namespace FlightLogNet.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Entities;
    using FlightLogNet.Repositories.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly IMapper mapper;

        public AirplaneRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public long AddGuestAirplane(AirplaneModel airplaneModel)
        {
            using var dbContext = new LocalDatabaseContext();

            Airplane airplane = new Airplane
            {
                GuestAirplaneImmatriculation = airplaneModel.Immatriculation,
                GuestAirplaneType = airplaneModel.Type,
            };

            dbContext.Airplanes.Add(airplane);
            dbContext.SaveChanges();
            return airplane.Id;
        }

        public IList<AirplaneModel> GetClubAirplanes()
        {
            using var dbContext = new LocalDatabaseContext();

            var airplanes = dbContext.ClubAirplanes
                .Include(airplane => airplane.AirplaneType);

            return this.mapper.ProjectTo<AirplaneModel>(airplanes).ToList();
        }

        public bool TryGetAirplane(AirplaneModel airplaneModel, out long airplaneId)
        {
            using var dbContext = new LocalDatabaseContext();

            var firstAirplane = dbContext.Airplanes.FirstOrDefault(airplane => airplane.Id == airplaneModel.Id);
            if (firstAirplane != null)
            {
                airplaneId = firstAirplane.Id;
                return true;
            }
            else
            {
                airplaneId = 0;
                return false;
            }
        }
    }
}
