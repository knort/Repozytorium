using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOR.Api.Controllers;
using SOR.BLL;
using SOR.Constants;
using SOR.DAL;
using SOR.Model;
using SOR.Repository;

namespace SOR.Api
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
            services.AddDbContext<SORDBContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("SOR")));
            services.AddIdentity<SORUser, IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<SORDBContext>();
            services.AddTransient<IRepository<Food>, Repository<Food>>();
            services.AddTransient<IRepository<Reservation>, Repository<Reservation>>();
            services.AddTransient<IRepository<Table>, Repository<Table>>();
            services.AddTransient<IRepository<Menu>, Repository<Menu>>();
            services.AddTransient<IRepository<MenuFood>, Repository<MenuFood>>();
            services.AddTransient<IBaseService<Food>, BaseService<Food>>();
            services.AddTransient<IBaseService<Menu>, BaseService<Menu>>();
            services.AddTransient<IBaseService<Table>, BaseService<Table>>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IBaseService<MenuFood>, BaseService<MenuFood>>();
            services.AddTransient<IBaseService<Reservation>, BaseService<Reservation>>();

            services.AddTransient<IUserService, UserService>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                var jwtSettingsSection = Configuration.GetSection("JwtSettings");

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettingsSection.GetValue(typeof(string), "Issuer").ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettingsSection.GetValue(typeof(string), "Secret").ToString())),
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Constants.ADMIN_POLICY, policy => policy.RequireRole(Constants.Constants.ADMIN));
                options.AddPolicy(Constants.Constants.USER_POLICY, policy => policy.RequireRole(Constants.Constants.USER, Constants.Constants.ADMIN));
            });
            services.AddSingleton(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy(Constants.Constants.CORS_POLICY, builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            });
            services.AddAutoMapper();
            services.AddLogging(configure =>
            {
                configure.AddConsole();
            }).AddTransient<UserController>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.AddEfDiagrams<SORDBContext>();
            app.UseCors(Constants.Constants.CORS_POLICY);
            app.UseAuthentication();
            app.UseMvc();
        }

    }
}
