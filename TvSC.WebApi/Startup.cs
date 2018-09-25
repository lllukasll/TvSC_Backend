using System;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TvSC.Data.DbModels;
using TvSC.Repo;
using TvSC.Repo.Interfaces;
using TvSC.Repo.Repositories;
using TvSC.Services.Interfaces;
using TvSC.Services.Services;

namespace TvSC.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("TvSC.Repo")));
            
            services.AddAutoMapper();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITvShowService, TvShowService>();
            services.AddTransient<ISeasonService, SeasonService>();
            services.AddTransient<IEpisodeService, EpisodeService>();
            services.AddTransient<ICalendarService, CalendarService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddIdentityCore<User>(options => { });
            services.AddScoped<IUserStore<User>, UserOnlyStore<User, DataContext>>();
            services.AddAuthentication("Identity.Application")
                .AddCookie("Identity.Application");

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Events.OnRedirectToLogin = ctx =>
                {
                    if (ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 401;
                        return Task.FromResult<object>(null);
                    }
                    return Task.CompletedTask;
                };

                opt.Events.OnRedirectToAccessDenied = ctx => {
                    if (ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 403;
                        return Task.FromResult<object>(null);
                    }
                    return Task.CompletedTask;
                };

                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });

            services.AddMvc()
                .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddSwagger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();

            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseSwaggerWithApiExplorer(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "TvSC API";
                    document.Info.Description = "Tv Show Calendar API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Łukasz Biedrzycki",
                        Email = "L.Biedr@gmail.com",
                        Url = string.Empty
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            app.UseStaticFiles();
        }
    }
}
