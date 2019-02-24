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
using SvishtovHighSchool.Application;
using SvishtovHighSchool.Application.Handlers.Course;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Domain.CourseModule.Commands;
using SvishtovHighSchool.EventStore;
using SvishtovHighSchool.Integration.Sender;
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

            DiRegistrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEventStoreConnection eventStoreConnection)
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

            eventStoreConnection.ConnectAsync().Wait();
        }

        private void DiRegistrations(IServiceCollection services)
        {
            // TODO check EventStoreEventStore registration
            services.AddSingleton(x => EventStoreConnection.Create(new Uri(Configuration.GetSection("EventStore:Connection").Value)));
            services.AddSingleton<IEventStore, EventStoreEventStore>();

            // TODO check Mongo registration
            services.AddSingleton(x => new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value));
            services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(Configuration.GetSection("MongoConnection:Database").Value));

            services.AddTransient<SvishtovHighSchoolDbContext>();
            services.AddTransient<ICourseRepository, MongoDbCourseRepository>();
            services.AddTransient<IReadOnlyCourseRepository, MongoDbCourseRepository>();

            services.AddTransient(typeof(IDomainRepository<>), typeof(DomainRepository<>));

            services.AddMediatR(typeof(CourseCreatorHandler).Assembly, typeof(CreateCourseCommand).Assembly);

            services.AddTransient<ISender, Sender>();
        }
    }
}
