namespace FlightLogNet
{
    using System;
    using System.Diagnostics;

    using Models;
    using Repositories;
    using Repositories.Entities;
    
    using Microsoft.Extensions.Configuration;

    internal static class TestDatabaseGenerator
    {
        internal static void RenewDatabase(IConfiguration configuration)
        {
            DeleteOldDatabase(configuration);
            CreateTestDatabaseWithFixedTime(DateTime.Now, configuration);
        }

        public static void DeleteOldDatabase(IConfiguration configuration)
        {
            using var dbContext = new LocalDatabaseContext(configuration);
            dbContext.Database.EnsureDeleted();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLiteralTypo", Justification = "This is test data.")]
        public static void CreateTestDatabaseWithFixedTime(DateTime now, IConfiguration configuration)
        {
            using var dbContext = new LocalDatabaseContext(configuration);
            bool newCreated = dbContext.Database.EnsureCreated();

            if (!newCreated)
            {
                Debug.WriteLine(@"Error: Database not created!");
                return;
            }

            InitializeDatabase(configuration);

            dbContext.Persons.AddRange(
                new Person { FirstName = "Kamila", LastName = "Spoustová", Address = null },
                new Person { FirstName = "Naděžda", LastName = "Pavelková", Address = null },
                new Person { FirstName = "Silvie", LastName = "Hronová", Address = null, Id = 103 },
                new Person { FirstName = "Miloš", LastName = "Korbel", Address = null },
                new Person { FirstName = "Petr", LastName = "Hrubec", Address = null, Id = 105 },
                new Person { FirstName = "Michal", LastName = "Vyvlečka", Address = null },
                new Person
                {
                    Id = 112,
                    FirstName = "Lenka",
                    LastName = "Kiasová",
                    Address = new Address { City = null, Country = null, PostalCode = null, Street = null }
                });

            dbContext.Airplanes.AddRange(
                new Airplane { Id = 2, GuestAirplaneImmatriculation = "OK-B128", GuestAirplaneType = "L-13A Blaník" },
                new Airplane { Id = 1, GuestAirplaneImmatriculation = "OK-V23428", GuestAirplaneType = "Zlín Z-42M" });

            dbContext.Flights.AddRange(
                new Flight
                {
                    Id = 1,
                    TakeoffTime = now.AddMinutes(-10),
                    LandingTime = null,
                    Airplane = dbContext.Airplanes.Find(1L),
                    Pilot = dbContext.Persons.Find(112L),
                    Copilot = null,
                    Task = "VLEK",
                    Type = FlightType.Glider
                },
                new Flight
                {
                    Id = 4,
                    TakeoffTime = now.AddMinutes(-10),
                    LandingTime = null,
                    Airplane = dbContext.Airplanes.Find(2L),
                    Pilot = dbContext.Persons.Find(103L),
                    Copilot = null,
                    Task = "Tahac",
                    Type = FlightType.Towplane
                },
                new Flight
                {
                    Id = 24057,
                    TakeoffTime = now.AddMinutes(-100),
                    LandingTime = null,
                    Airplane = dbContext.Airplanes.Find(1L),
                    Pilot = dbContext.Persons.Find(105L),
                    Copilot = null,
                    Task = "VLEK",
                    Type = FlightType.Glider
                },
                new Flight
                {
                    Id = 24058,
                    TakeoffTime = now.AddMinutes(-100),
                    LandingTime = null,
                    Airplane = dbContext.Airplanes.Find(2L),
                    Pilot = dbContext.Persons.Find(103L),
                    Copilot = null,
                    Task = "Tahac",
                    Type = FlightType.Towplane
                },
                new Flight
                {
                    Id = 444,
                    TakeoffTime = new DateTime(2020, 1, 7, 16, 47, 10),
                    LandingTime = new DateTime(2020, 1, 7, 17, 17, 10),
                    Airplane = dbContext.Airplanes.Find(2L),
                    Pilot = dbContext.Persons.Find(112L),
                    Copilot = null,
                    Task = "Tahac",
                    Type = FlightType.Towplane
                });

            dbContext.FlightStarts.AddRange(
                new FlightStart
                {
                    Glider = dbContext.Flights.Find(1L),
                    Towplane = dbContext.Flights.Find(4L)
                },
                new FlightStart
                {
                    Glider = dbContext.Flights.Find(24057L),
                    Towplane = dbContext.Flights.Find(24058L)
                },
                new FlightStart
                {
                    Towplane = dbContext.Flights.Find(444L)
                });

            dbContext.SaveChanges();
        }

        public static void InitializeDatabase(IConfiguration configuration)
        {
            using var dbContext = new LocalDatabaseContext(configuration);

            dbContext.ClubAirplanes.AddRange(
                new ClubAirplane
                {
                    Id = 2,
                    Immatriculation = "OK-B123",
                    AirplaneType = new AirplaneType { Type = "L-13A Blaník" }
                },
                new ClubAirplane
                {
                    Id = 1,
                    Immatriculation = "OK-V23424",
                    AirplaneType = new AirplaneType { Type = "Zlín Z-42M" }
                });

            dbContext.SaveChanges();
        }
    }
}
