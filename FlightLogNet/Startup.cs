namespace FlightLogNet
{
    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        private const string AllowedOrigins = "AllowedOrigins";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InjectConfiguration.Initialization(services);
            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // services.AddAutoMapper(System.Reflection.Assembly.GetCallingAssembly());
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy => {
                        string[] origins = this.Configuration.GetSection(AllowedOrigins).Get<string[]>();
                        if (origins is null)
                        {
                            policy.AllowAnyOrigin();
                        }
                        else
                        {
                            policy.WithOrigins(origins);
                        }
                    });
            });
        }

        // ReSharper disable once UnusedMember.Global - used by Framework
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            app.UseDefaultFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                TestDatabaseGenerator.RenewDatabase(this.Configuration);
            }
            else
            {
                using var dbContext = new Repositories.LocalDatabaseContext(this.Configuration);
                bool newlyCreated = dbContext.Database.EnsureCreated();
                if (newlyCreated)
                {
                    TestDatabaseGenerator.InitializeDatabase(this.Configuration);
                }
            }
            
            app.UseRouting();
            app.UseCors();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
