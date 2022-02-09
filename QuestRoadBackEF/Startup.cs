using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestRoadBackEF.Contracts;
using QuestRoadBackEF.Repositories;
using QuestRoadLibrary;
using QuestRoadLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBackEF
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
            

            services.AddDbContext<QuestRoadContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddControllers();

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = authOptions.Issuer,
                       ValidateAudience = true,
                       ValidAudience = authOptions.Audience,
                       ValidateLifetime = true,
                       IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                       ValidateIssuerSigningKey = true,


                   };
               });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuestRoadBackEF", Version = "v1" });
            });

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IQuestRepository, QuestRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IHelpRepository, HelpRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuestRoadBackEF v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
        }
    }
}
