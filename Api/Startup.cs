using Api.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using RapidFireLib;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace API
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
            services.AddRapidFire(new AppConfig());

            services.AddCors(options =>
            {
                options.AddPolicy("AllOrigins",
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = 
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        @"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIgGivly4ABfZkrDr1RKcYEI8Oyi
                        9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+YqS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3g
                        ukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==")),
                    ValidIssuer = "",
                    ValidAudience = "",
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = 
            new DefaultContractResolver()).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "api/{controller}/{action}/{id?}");
            });
        }
    }
}