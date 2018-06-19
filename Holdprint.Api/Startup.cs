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
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using GlobalExceptionHandler.WebApi;
using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

            ConfigureSwagger(services);
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Holdprint",
                        Version = "v1",
                        Description = "Holdprint API REST criada com o ASP.NET Core",
                        Contact = new Contact
                        {
                            Name = "Fabricio Dupont",
                            Url = "https://github.com/fabriciogd"
                        }
                    });

                string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Holdprint");
            });

            ConfigureGlobalExceptionHandler(app);

            AutoMapperConfiguration.Configure();
        }

        public void ConfigureGlobalExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler().WithConventions(x =>
            {
                x.ForException<Exception>().ReturnStatusCode(StatusCodes.Status400BadRequest);
                x.MessageFormatter(exception => JsonConvert.SerializeObject(new
                {
                    error = new
                    {
                        message = "Something went wrong",
                        statusCode = StatusCodes.Status400BadRequest
                    }
                }));
            });
        }
    }
}
