using System.Diagnostics;

using FlightLogNet.Models;

namespace FlightLogNet
{
    using System;

    using FlightLogNet.Repositories;
    using FlightLogNet.Repositories.Entities;

    internal static class TestDatabaseGenerator
    {
        internal static void RenewDatabase()
        {
            DeleteOldDatabase();
            CreateTestDatabaseWithFixedTime(DateTime.Now);
        }

        public static void DeleteOldDatabase()
        {
            using var dbContext = new LocalDatabaseContext();
            dbContext.Database.EnsureDeleted();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLiteralTypo", Justification = "This is test data.")]
        public static void CreateTestDatabaseWithFixedTime(DateTime now)
        {
            using var dbContext = new LocalDatabaseContext();
            bool newCreated = dbContext.Database.EnsureCreated();

            if (!newCreated)
            {
                Debug.WriteLine(@"Error: Database not created!");
                return;
            }

            dbContext.Persons.AddRange(
                new Person { FirstName = "Kamila", LastName = "Spoustová", Address = null },
                new Person { FirstName = "Naděžda", LastName = "Pavelková", Address = null },
                new Person { FirstName = "Silvie", LastName = "Hronová", Address = null, Id = 3 },
                new Person { FirstName = "Miloš", LastName = "Korbel", Address = null },
                new Person { FirstName = "Petr", LastName = "Hrubec", Address = null, Id = 5 },
                new Person { FirstName = "Michal", LastName = "Vyvlečka", Address = null },
                new Person
                {
                    Id = 12,
                    FirstName = "Lenka",
                    LastName = "Kiasová",
                    Address = new Address { City = null, Country = null, PostalCode = null, Street = null }
                });

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

            dbContext.Airplanes.AddRange(
                new Airplane { Id = 2, GuestAirplaneImmatriculation = "OK-B123", GuestAirplaneType = "L-13A Blaník" },
                new Airplane { Id = 1, GuestAirplaneImmatriculation = "OK-V23424", GuestAirplaneType = "Zlín Z-42M" });

            dbContext.Flights.AddRange(
                new Flight
                {
                    Id = 1,
                    TakeoffTime = now.AddMinutes(-10),
                    LandingTime = null,
                    Airplane = dbContext.Airplanes.Find(1L),
                    Pilot = dbContext.Persons.Find(12L),
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
                    Pilot = dbContext.Persons.Find(3L),
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
                    Pilot = dbContext.Persons.Find(5L),
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
                    Pilot = dbContext.Persons.Find(3L),
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
                    Pilot = dbContext.Persons.Find(12L),
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
    }
}