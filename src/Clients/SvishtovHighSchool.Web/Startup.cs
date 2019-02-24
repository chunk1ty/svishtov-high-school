using System;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SvishtovHighSchool.Application.Handlers.Commands;
using SvishtovHighSchool.Application.Handlers.Events;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.EventStore;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.ReadModel;
using SvishtovHighSchool.ReadModel.Contracts;
using SvishtovHighSchool.ReadModel.Repositories;


namespace SvishtovHighSchool.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR(typeof(CourseCreatorHandler).Assembly, typeof(CreateCourseCommand).Assembly);

            DbRegistrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void DbRegistrations(IServiceCollection services)
        {
            //services.AddSingleton(x => EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113")));
            services.AddSingleton<IEventStore, EventStoreEventStore>();

            services.AddScoped(x => new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value));
            services.AddScoped(x => x.GetService<MongoClient>().GetDatabase(Configuration.GetSection("MongoConnection:Database").Value));
            services.AddScoped<SvishtovHighSchoolDbContext>();
            services.AddScoped<ICourseRepository, MongoDbCourseRepository>();
            services.AddScoped<IReadOnlyCourseRepository, MongoDbCourseRepository>();

            services.AddScoped<ICommandSender, FakeBus>();
            services.AddScoped<IEventPublisher, FakeBus>();
            services.AddScoped<CourseCreatedHandler>();

            services.AddScoped(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            
        }
    }
}
