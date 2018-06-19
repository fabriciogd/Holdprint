using Holdprint.Core.Contract;
using Holdprint.Core.Implementation;
using Holdprint.EF.Contract;
using Holdprint.EF.Implementation;
using Holdprint.Mappers.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Holdprint.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("HoldprintDatabase"));

            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryQuery<,>), typeof(RepositoryQuery<,>));
            services.AddTransient(typeof(IService<,>), typeof(Service<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            AutoMapperConfiguration.Configure();
        }
    }
}
