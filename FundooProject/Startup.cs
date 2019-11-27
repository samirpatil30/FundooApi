// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooProject
{
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using BusinessLayer.Services;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using RepositoryLayer.Services;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        //// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserRegistrationBusiness, UserRegistrationService>();
            services.AddTransient<IUserRegistraionRepository, UserRegistrationRepository>();

            services.AddTransient<IUserNotesBusiness, UserNotesBusiness>();
            services.AddTransient<INotesRepository, UserNotesRepository>();

            services.AddTransient<ILabelBussinessManager, LabelBussinessManager>();
            services.AddTransient<ILabelRepositoryManager, LabelRepositoryManager>();

            services.AddDbContext<AuthenticationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("connectionDb")));

            services.AddDefaultIdentity<ApplicationUser>()
                 .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(g =>
            {
                g.RequireHttpsMetadata = false;
                g.SaveToken = false;
                g.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                };
            });

            // Register the Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.OperationFilter<FileUploadedOperation>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app">app</param>
        /// <param name="env">env</param>
        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable or used middleware to serve Swagger 
            app.UseSwagger();

            // Enable swagger-ui
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        /// <summary>
        /// FileUploadedOperation
        /// </summary>
        /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
        public class FileUploadedOperation : IOperationFilter
        {
            /// <summary>
            /// Applies the specified swagger document.
            /// </summary>
            /// <param name="swaggerDocument">The swagger document.</param>
            /// <param name="documentFilter">The document filter.</param>
            public void Apply(Operation swaggerDocument, OperationFilterContext documentFilter)
            {
                if (swaggerDocument.Parameters == null)
                {
                    swaggerDocument.Parameters = new List<IParameter>();
                }

                swaggerDocument.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = true
                });
            }
        }   
    }
}