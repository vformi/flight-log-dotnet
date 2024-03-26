namespace FlightLogNet.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            InjectConfiguration.Initialization(services);
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
