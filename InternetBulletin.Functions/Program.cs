// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Program class from where the execution starts</summary>
// *********************************************************************************

namespace InternetBulletin.Functions
{
	using Azure.Identity;
	using Microsoft.Azure.Functions.Worker;
	using Microsoft.Azure.Functions.Worker.Builder;
	using Microsoft.Extensions.DependencyInjection;
	using InternetBulletin.Functions.Dependencies;
	using Microsoft.Extensions.Hosting;
	using static InternetBulletin.Shared.Constants.ConfigurationConstants;
	using Microsoft.Extensions.Configuration;

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
			var builder = FunctionsApplication.CreateBuilder(args);
			if (builder.Environment.IsDevelopment())
			{
				builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile(LocalAppsettingsFileConstant, optional: true)
					.AddEnvironmentVariables();
			}

			var credentials = builder.Environment.IsDevelopment()
				? new DefaultAzureCredential()
				: new DefaultAzureCredential(new DefaultAzureCredentialOptions
				{
					ManagedIdentityClientId = builder.Configuration[ManagedIdentityClientIdConstant]
				});

			builder.ConfigureFunctionsWebApplication();
			builder.ConfigureAzureAppConfiguration(credentials);
			builder.ConfigureAzureSqlServer();
			builder.ConfigureFunctionServices();

			builder.Services
				.AddApplicationInsightsTelemetryWorkerService()
				.ConfigureFunctionsApplicationInsights();

			builder.Build().Run();
		}
	}
}

