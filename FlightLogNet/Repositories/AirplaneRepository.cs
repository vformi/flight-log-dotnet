namespace FlightLogNet.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Entities;
    using FlightLogNet.Repositories.Interfaces;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        
        public AirplaneRepository(IMapper mapper, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public long AddGuestAirplane(AirplaneModel airplaneModel)
        {
            using var dbContext = new LocalDatabaseContext(this.configuration);

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
            using var dbContext = new LocalDatabaseContext(this.configuration);

            var airplanes = dbContext.ClubAirplanes
                .Include(airplane => airplane.AirplaneType);

            return this.mapper.ProjectTo<AirplaneModel>(airplanes).ToList();
        }

        public bool TryGetAirplane(AirplaneModel airplaneModel, out long airplaneId)
        {
            using var dbContext = new LocalDatabaseContext(this.configuration);

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
