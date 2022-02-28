using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TV.DAL.Entities;
using TV.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TV.SER;
using TV.SER.Interfaces;
using TV.SER.DTOs;
using TV.SER.Factories;
using System;
using TV.API.Helpers.Mappers;

namespace TV.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 4;

                opt.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);

            builder.AddEntityFrameworkStores<ApplicationDbContex>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();
            builder.AddDefaultTokenProviders();

            services.AddAuthorization(options =>
                options.AddPolicy("ClientPolicy",
                policy => policy.RequireRole("Client")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                              .GetBytes(Configuration.GetSection("AppSettings:Key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

            services.AddDbContext<TV.DAL.ApplicationDbContex>(options =>
                 options.UseSqlServer(Configuration["Database:ConnectionString"]));
            services.AddControllers();
            services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                                    builder =>
                                    {
                                        builder.WithOrigins("http://localhost:4200");
                                    });
                });

            services.AddSingleton<IEmail, MailJet>();
            services.AddSingleton<ICloudStorage, AzureStorage>();
            services.AddSingleton<IStorageConnectionFactory, StorageConnectionFactory>(sp =>
           {
               CloudStorageOptionsDTO cloudStorageOptionsDTO = new CloudStorageOptionsDTO();
               cloudStorageOptionsDTO.ConnectionString = Configuration["AzureBlobStorage:ConnectionString"];
               cloudStorageOptionsDTO.ProfilePicsContainer = Configuration["AzureBlobStorage:BlobContainer"];
               return new StorageConnectionFactory(cloudStorageOptionsDTO);

           });
            services.Configure<EmailOptionsDTO>(Configuration.GetSection("MailJet"));
            services.AddSwaggerGen(s =>
                {
                    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Transportadora Vieir4ndo",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Matheus Vieira Santos",
                            Url = new Uri("https://github.com/vieir4ndo")
                        }
                    });
                });
            services.AddAutoMapper(typeof(UserMappers));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

              app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "swagger";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
