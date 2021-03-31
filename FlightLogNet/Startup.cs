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
        }

        // ReSharper disable once UnusedMember.Global - used by Framework
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            // Security headers make me happy
            app.UseHsts(options => options.MaxAge(365).IncludeSubdomains());
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());

            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .StyleSources(s => s.Self())
                .StyleSources(s => s.UnsafeInline())
                .FontSources(s => s.Self())
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self())
                .ScriptSources(s => s.Self()));
            // End Security Headers

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                TestDatabaseGenerator.RenewDatabase();
            }
            else
            {
                using var dbContext = new Repositories.LocalDatabaseContext();
                dbContext.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
