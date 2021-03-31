namespace FlightLogNet.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Entities;
    using FlightLogNet.Repositories.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class FlightRepository : IFlightRepository
    {
        private readonly IMapper mapper;

        public FlightRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        // TODO 2.1: Upravte metodu tak, aby vrátila pouze lety specifického typu
        public IList<FlightModel> GetAllFlights()
        {
            using var dbContext = new LocalDatabaseContext();

            var flights = dbContext.Flights;

            return this.mapper.ProjectTo<FlightModel>(flights).ToList();
        }

        // TODO 2.3: Vytvořte metodu, která načte letadla, která jsou ve vzduchu, seřadí je od nejstarších,
        // a v případě shody dá vlečné pred kluzák, který táhne

        public void LandFlight(FlightLandingModel landingModel)
        {
            using var dbContext = new LocalDatabaseContext();

            var flight = dbContext.Flights.Find(landingModel.FlightId);
            flight.LandingTime = landingModel.LandingTime;
            dbContext.SaveChanges();
        }

        public void TakeoffFlight(long? gliderFlightId, long? towplaneFlightId)
        {
            using var dbContext = new LocalDatabaseContext();

            var flightStart = new FlightStart
            {
                Glider = dbContext.Flights.Find(gliderFlightId),
                Towplane = dbContext.Flights.Find(towplaneFlightId),
            };

            dbContext.FlightStarts.Add(flightStart);
            dbContext.SaveChanges();
        }

        public long CreateFlight(CreateFlightModel model)
        {
            using var dbContext = new LocalDatabaseContext();

            var copilot = model.CopilotId != null
                ? dbContext.Persons.Find(model.CopilotId)
                : null;

            var flight = new Flight
            {
                Airplane = dbContext.Airplanes.Find(model.AirplaneId),
                Copilot = copilot,
                Pilot = dbContext.Persons.Find(model.PilotId),
                TakeoffTime = model.TakeOffTime,
                Task = model.Task
            };

            dbContext.Flights.Add(flight);
            dbContext.SaveChanges();

            return flight.Id;
        }

        public IList<ReportModel> GetReport()
        {
            using var dbContext = new LocalDatabaseContext();

            var flightStarts = dbContext.FlightStarts
                .Include(flight => flight.Glider)
                .Include(flight => flight.Glider.Airplane)
                .Include(flight => flight.Glider.Pilot)
                .Include(flight => flight.Glider.Copilot)
                .Include(flight => flight.Towplane)
                .Include(flight => flight.Towplane.Airplane)
                .Include(flight => flight.Towplane.Pilot)
                .Include(flight => flight.Towplane.Copilot)
                .OrderByDescending(start => start.Towplane.TakeoffTime);

            return this.mapper.ProjectTo<ReportModel>(flightStarts).ToList();
        }
    }
}
