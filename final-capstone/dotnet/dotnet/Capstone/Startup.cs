using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Capstone.DAO;
using Capstone.Security;
using Capstone.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Capstone.DAO.Lease;
using Capstone.DAO.Renter;
using Capstone.DAO.Maintenance;
using Capstone.DAO.Transaction;
using Capstone.DAO.Landlord;
using Capstone.Controllers;

namespace Capstone
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            string connectionString = Configuration.GetConnectionString("Project");
            services.AddDbContext<final_capstoneContext>(options => options.UseSqlServer(connectionString));
            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtSecret"]);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = "sub";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    NameClaimType = "name"
                };
            });

            // Dependency Injection configuration
            services.AddSingleton<ITokenGenerator>(tk => new JwtGenerator(Configuration["JwtSecret"]));
            services.AddSingleton<IPasswordHasher>(ph => new PasswordHasher());
            services.AddTransient<IUserDAO>(m => new UserSqlDAO(connectionString));
            services.AddTransient<IPropertyDAO>(m => new PropertySqlDAO(connectionString));
            services.AddTransient<ILeaseDAO>(m => new LeaseSqlDAO(connectionString));
            services.AddTransient<IRenterDAO>(m => new RenterSqlDAO(connectionString));
            services.AddTransient<ILandlordDAO>(m => new LandlordSqlDAO(connectionString));
            services.AddTransient<ITransactionDAO>(m => new TransactionSqlDAO(connectionString));
            services.AddTransient<IMaintenanceDAO>(m => new MaintenanceSqlDAO(connectionString));
            services.AddScoped<ILeaseService, LeaseService>();
            services.AddScoped<IRenterService, RenterService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();

            services.AddAutoMapper(typeof(Startup));
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
            app.UseMvc();
        }
    }
}
