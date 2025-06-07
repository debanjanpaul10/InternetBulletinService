// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Dependencies
{
	using InternetBulletin.API.Middleware;
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Business.Services;
	using InternetBulletin.Data;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.DataServices;
	using InternetBulletin.Shared.Helpers;
	using Microsoft.EntityFrameworkCore;
	using MongoDB.Driver;
	using static InternetBulletin.Shared.Constants.ConfigurationConstants;

	/// <summary>
	/// The Dependency Injection Container Class.
	/// </summary>
	public static class DIContainer
	{
		/// <summary>
		/// Configures the application dependencies.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="services">The services.</param>
		public static void ConfigureAzureSqlServer(this WebApplicationBuilder builder)
		{
			var sqlConnectionString = builder.Environment.IsDevelopment()
				? builder.Configuration[LocalSqlConnectionStringConstant]
				: builder.Configuration[SqlConnectionStringConstant];
			if (!string.IsNullOrEmpty(sqlConnectionString))
			{
				builder.Services.AddDbContext<SqlDbContext>(options =>
				{
					options.UseSqlServer
					(
						connectionString: sqlConnectionString,
						options => options.EnableRetryOnFailure
						(
							maxRetryCount: 3,
							maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null
						)
					);
				});
			}
		}

		/// <summary>
		/// Configures the MongoDB service.
		/// </summary>
		/// <param name="builder">The web application builder.</param>
		public static void ConfigureMongoDbServer(this WebApplicationBuilder builder)
		{
			var mongoConnectionString = builder.Configuration[MongoDbConnectionStringConstant];
			if (!string.IsNullOrEmpty(mongoConnectionString))
			{
				var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
				settings.SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
				builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));
			}
		}

		/// <summary>
		/// Configures the helper service dependencies.
		/// </summary>
		/// <param name="services">The service collection.</param>
		public static void ConfigureHelperServiceDependencies(this IServiceCollection services)
		{
			services.AddScoped<IHttpClientHelper, HttpClientHelper>();
			services.AddScoped<ICacheService, CacheService>();
			services.AddScoped<GlobalExceptionFilter>();
		}

		/// <summary>
		/// Configures business manager dependencies.
		/// </summary>
		/// <param name="builder">The service collection.</param>
		public static void ConfigureBusinessManagerDependencies(this IServiceCollection services)
		{
			services.AddScoped<IPostsService, PostsService>();
			services.AddScoped<IProfilesService, ProfilesService>();
			services.AddScoped<IPostRatingsService, PostRatingsService>();
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IAIService, AIService>();
			services.AddScoped<IBulletinService, BulletinService>();
		}

		/// <summary>
		/// Configures data manager dependencies.
		/// </summary>
		/// <param name="builder">The service collection.</param>
		public static void ConfigureDataManagerDependencies(this IServiceCollection services)
		{
			services.AddScoped<IPostsDataService, PostsDataService>();
			services.AddScoped<IProfilesDataService, ProfilesDataService>();
			services.AddScoped<IPostRatingsDataService, PostRatingsDataService>();
			services.AddScoped<IUsersDataService, UsersDataService>();
			services.AddScoped<IAIDataService, AIDataService>();
		}
	}
}
