using Microsoft.EntityFrameworkCore;
using WebApi.Bindings;
using WebApi.Data;
using WebApi.Models;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostEnvironment HostEnvironment { get; private set; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            HostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBindings();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MyDb"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            PopulateDatabase(serviceProvider.GetService<UserDbContext>()!);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseSwagger()
                .UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("../swagger/v1/swagger.json", "UsersService");
                    x.EnableTryItOutByDefault();
                })
                .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });
        }

        private static void PopulateDatabase(UserDbContext context)
        {
            for (int i = 0; i < DummyData.Users.Count; i++)
            {
                context.Add(DummyData.Users[i]);
            }
            context.SaveChanges();
        }
    }
}