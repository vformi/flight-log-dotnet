namespace FlightLogNet.Repositories
{
    using FlightLogNet.Repositories.Entities;

    using Microsoft.EntityFrameworkCore;

    public class LocalDatabaseContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<ClubAirplane> ClubAirplanes { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<FlightStart> FlightStarts { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=local.db");
    }
}
