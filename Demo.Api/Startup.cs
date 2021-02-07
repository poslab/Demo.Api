using AutoMapper;
using Demo.Api.Mapping;
using Demo.Repository;
using Demo.Repository.Interfaces;
using Demo.Services;
using Demo.Services.Interfaces;
using Demo.Services.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Demo.Api
{
    public class Startup
    {
        private IDbConnection Database;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Create database connection
            Database = new SqlConnection(configuration.GetConnectionString("DemoDB"));
            //Database.Open();  No Real DB, the is hardcoded for this demo. 

            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(Configuration)
              .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        ValidAudience = Configuration["JwtSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]))
                    };
                });

            services.AddSingleton(Configuration);
            services.AddSingleton(Database);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions().Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));


            services.AddScoped(typeof(IContactService), typeof(ContactService));

            services.AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddTransient(typeof(IContactRepository<,>), typeof(ContactRepository<,>));


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ViewModelProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configure Serilog
            loggerFactory.AddSerilog();

            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
