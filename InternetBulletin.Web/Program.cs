// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Program class from where the execution starts</summary>
// *********************************************************************************

namespace InternetBulletin.Web
{
    using InternetBulletin.Web.Configuration;
    using InternetBulletin.Web.Helpers;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;
    using Polly;
    using Polly.Extensions.Http;
    using Azure.Identity;
    using Microsoft.Extensions.FileProviders;
    using System.Globalization;
    using InternetBulletin.Shared.Constants;

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
                .AddJsonFile(LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();

            var miCredentials = builder.Configuration[ManagedIdentityClientIdConstant];
            var credentials = builder.Environment.IsDevelopment()
                ? new DefaultAzureCredential()
                : new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = miCredentials
                });

            builder.ConfigureAzureAppConfig(credentials);
            builder.ConfigureServices();

            var app = builder.Build();
            app.ConfigureApplication();
        }

        /// <summary>
        /// Configures services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
            builder.Services.AddTransient<TokenHelper>();
            builder.ConfigureHttpClientServices();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// Configures http client services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void ConfigureHttpClientServices(this WebApplicationBuilder builder)
        {
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            var apimUrl = builder.Configuration[ApimBaseUrlConstant];

            var webApiUrl = builder.Environment.IsDevelopment()
                ? builder.Configuration[LocalWebApiBaseAddressConstant]
                : apimUrl;

            if (!string.IsNullOrEmpty(webApiUrl))
            {
                builder.Services.AddHttpClient(BulletinHttpClientConstant, client =>
                {
                    client.BaseAddress = new Uri(webApiUrl);
                    client.Timeout = TimeSpan.FromMinutes(3);
                }).AddPolicyHandler(retryPolicy);
            }

            var aiApimUrl = string.Format(CultureInfo.CurrentCulture, RouteConstants.AiApimUrl, apimUrl);
            if (!string.IsNullOrEmpty(aiApimUrl))
            {
                builder.Services.AddHttpClient(BulletinAiHttpClientConstant, client =>
                {
                    client.BaseAddress = new Uri(string.Format(aiApimUrl));
                    client.Timeout = TimeSpan.FromMinutes(3);
                }).AddPolicyHandler(retryPolicy);
            }
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureApplication(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(app.Environment.ContentRootPath, "dist")
                ),
                RequestPath = "/dist"
            });
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=InternetBulletinWeb}/{action=Index}/{id?}");

            app.Run();
        }

    }
}


