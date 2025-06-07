// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Program class from where the execution starts</summary>
// *********************************************************************************

namespace InternetBulletin.API
{
    using Azure.Identity;
    using InternetBulletin.API.Dependencies;
    using InternetBulletin.API.Middleware;
    using Microsoft.OpenApi.Models;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Program class from where the execution starts
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(LocalAppsettingsFileConstant, optional: true)
                .AddEnvironmentVariables();

            var miCredentials = builder.Configuration[ManagedIdentityClientIdConstant];
            var credentials = builder.Environment.IsDevelopment()
                ? new DefaultAzureCredential() : new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = miCredentials
                });

            builder.ConfigureAzureAppConfiguration(credentials);
            builder.ConfigureApiServices();
            builder.Services.ConfigureServices();

            var app = builder.Build();
            app.ConfigureApplication();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Internet Bulletin API",
                    Version = "v1",
                    Description = "API Documentation for Internet Bulletin",
                    Contact = new OpenApiContact
                    {
                        Name = "Debanjan Paul",
                        Email = "debanjanpaul10@gmail.com"
                    }
                });
            });
            services.AddMvc(options =>
            {
                options.Filters.AddService<GlobalExceptionFilter>();
            });
            services.AddHttpContextAccessor();
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Internet Bulletin API v1");
                    c.RoutePrefix = "swaggerui";
                });
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}


