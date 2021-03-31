namespace FlightLogNet.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            InjectConfiguration.Initialization(services);
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
