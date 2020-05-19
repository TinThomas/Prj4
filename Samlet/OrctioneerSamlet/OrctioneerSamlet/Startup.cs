using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VareDatabase;
using VareDatabase.Controllers;
using VareDatabase.DBContext;
using VareDatabase.Interfaces;
using VareDatabase.Interfaces.Auction;
using VareDatabase.Repo;
using VareDatabase.Repo.Auction;
using VueCliMiddleware;

namespace OrctioneerSamlet
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    //SomeFancySecretKey
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

            });

            services.AddCors();
            services.AddDbContext<VareDataModelContext>();
            services.AddDbContext<UserModelContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));
            services.AddDbContext<PassModelContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PassConnection")));
            services.AddDbContext<WalletContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WalletConnection")));

            services.AddScoped<VareDataModelContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
            services.AddScoped<IUnitOfWork,AuctionUnitOfWork>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddSingleton<ItemEntityController>();
            services.AddSingleton<BidEntitiesController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserModelContext user, PassModelContext pass, WalletContext wallet)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            SeedUsers.seedUsers(user,pass, wallet);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
