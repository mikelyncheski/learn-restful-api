using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;


namespace RestfulApi
{
    public class Startup
    {
        // public Startup(IHostingEnvironment env)
        // {
        //     var builder = new ConfigurationBuilder()
        //         .SetBasePath(env.ContentRootPath)
        //         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //         .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //         .AddEnvironmentVariables();

        //     if (env.IsDevelopment())
        //     {
        //         // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
        //         builder.AddApplicationInsightsSettings(developerMode: true);
        //     }
        //     Configuration = builder.Build();
        // }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction => 
            {
                // Complain if unsupported media type is requested.  
                // Don't just return JSON (the default), return 406 Not Acceptable instead
                setupAction.ReturnHttpNotAcceptable = true;  

                // JSON is the only output format by default.  If you want XML or other to be the default,
                // insert it at the beginning of the list.
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            // var connection = @"data source=.;User Id=CoreUser;Password=eX@mlple879;database=HarrierCore;";  // TODO: Move to config file
            // services.AddDbContext<MyDBDataContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<RestfulApi.Entities.Author, RestfulApi.Models.AuthorDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Today.Year - src.BirthDate.Year));
     
                cfg.CreateMap<RestfulApi.Entities.Book, RestfulApi.Models.BookDto>();
                
                cfg.CreateMap<RestfulApi.Models.AuthorForAddDto, RestfulApi.Entities.Author>();
           });



            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            {
                // See http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context => 
                    {
                        // Do we have access to exception here?
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Sometimes bad things happen to really good people.");
                    });
                });
            }




            app.UseMvc();

            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });
        }


        // // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        // {
        //     loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        //     loggerFactory.AddDebug();

        //     app.UseApplicationInsightsRequestTelemetry();

        //     if (env.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //         app.UseBrowserLink();
        //     }
        //     else
        //     {
        //         app.UseExceptionHandler("/Home/Error");
        //     }

        //     app.UseApplicationInsightsExceptionTelemetry();

        //     app.UseStaticFiles();

        //      ASP.Net core team recommends using attribute based routing instead
        //     app.UseMvc(routes =>
        //     {
        //         routes.MapRoute(
        //             name: "default",
        //             template: "{controller=Home}/{action=Index}/{id?}");
        //     });
        // }
        
    }
}
