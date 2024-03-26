namespace FlightLogNet
{
    using Facades;
    using Integration;
    using Operation;
    using Repositories;
    using Repositories.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    internal static class InjectConfiguration
    {
        internal static void Initialization(IServiceCollection services)
        {
            services.AddScoped<IAirplaneRepository, AirplaneRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<TakeoffOperation, TakeoffOperation>();
            services.AddScoped<GetExportToCsvOperation, GetExportToCsvOperation>();
            services.AddScoped<LandOperation, LandOperation>();
            services.AddScoped<CreatePersonOperation, CreatePersonOperation>();

            services.AddScoped<AirplaneFacade, AirplaneFacade>();
            services.AddScoped<PersonFacade, PersonFacade>();
            services.AddScoped<FlightFacade, FlightFacade>();

            services.AddScoped<IClubUserDatabase, ClubUserDatabaseStub>();
            // services.AddScoped<IClubUserDatabase, ClubUserDatabase>();
        }
    }
}
