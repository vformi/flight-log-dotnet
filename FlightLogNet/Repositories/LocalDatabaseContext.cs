namespace FlightLogNet.Repositories
{
    using FlightLogNet.Repositories.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class LocalDatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;

        public LocalDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<ClubAirplane> ClubAirplanes { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<FlightStart> FlightStarts { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string sqliteConnectionString = this.configuration.GetValue<string>("SqliteConnectionString");
            string npgsqlConnectionString = this.configuration.GetValue<string>("NpgsqlConnectionString");

            if (sqliteConnectionString is not null)
            {
                options.UseSqlite(sqliteConnectionString);
            }
            else if (npgsqlConnectionString is not null)
            {
                options.UseNpgsql(npgsqlConnectionString);
            }
            else
            {
                options.UseSqlite("Data Source=local.db");
            }
        }
    }
}
