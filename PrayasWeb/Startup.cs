namespace PrayasWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthorization();
            app.UseSession();

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddSession();
            //services.AddSingleton<IEmployeeRepository, dBLayer >();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }
    }
}
