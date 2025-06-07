// *********************************************************************************
//	<copyright file="BulletinService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary> The Bulletin Service class.</summary>
// *********************************************************************************
namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs.ApplicationInfo;
	using Microsoft.Extensions.Configuration;
	using MongoDB.Bson;
	using MongoDB.Driver;
	using static InternetBulletin.Shared.Constants.IbbsConstants;

	/// <summary>
	/// The Bulletin Service class.
	/// </summary>
	/// <see cref="IBulletinService"/>
	public class BulletinService : IBulletinService
	{
		/// <summary>
		/// The mongo database.
		/// </summary>
		private readonly IMongoDatabase _mongoDatabase;

		/// <summary>
		/// The configuration.
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// The cache service.
		/// </summary>
		private readonly ICacheService _cacheService;

		/// <summary>
		/// Initializes a new instance of <see cref="BulletinService"/> class.
		/// </summary>
		/// <param name="mongoClient">The Mongo DB Client.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="cacheService">The cache service.</param>
		public BulletinService(IMongoClient mongoClient, IConfiguration configuration, ICacheService cacheService)
		{
			this._configuration = configuration;

			var databaseName = this._configuration[ConfigurationConstants.MongoDatabaseNameConstant];
			this._mongoDatabase = mongoClient.GetDatabase(databaseName);
			this._cacheService = cacheService;
		}

		/// <summary>
		/// Gets the about us information data asynchronously.
		/// </summary>
		/// <returns>The about us information data. <see cref="AboutUsAppInfoDataDTO"/></returns>
		/// <exception cref="Exception">The default exception thrown.</exception>
		public async Task<AboutUsAppInfoDataDTO> GetAboutUsDataAsync()
		{
			var cacheData = this._cacheService.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey);
			if (cacheData is not null)
			{
				return cacheData;
			}
			else
			{
				var applicationInformation = this._mongoDatabase.GetCollection<ApplicationInformation>(ApplicationInformationCollectionConstant);
				var applicationTechnologies = this._mongoDatabase.GetCollection<ApplicationTechnologies>(ApplicationTechnologiesCollectionConstant);
				if (applicationInformation is not null && applicationTechnologies is not null)
				{
					var applicationInfoResponseTask = applicationInformation.Find(_ => true).FirstAsync();
					var applicationTechnolgiesDataTask = applicationTechnologies.Find(new BsonDocument()).ToListAsync();
					await Task.WhenAll(applicationInfoResponseTask, applicationTechnolgiesDataTask).ConfigureAwait(false);

					var finalResult = new AboutUsAppInfoDataDTO
					{
						ApplicationTechnologiesData = applicationTechnolgiesDataTask.Result,
						ApplicationInformationData = applicationInfoResponseTask.Result
					};
					this._cacheService.SetCacheData(CacheKeys.AboutUsDataCacheKey, finalResult, TimeSpan.FromMinutes(30));
					return finalResult;
				}

				throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
			}
		}
	}
}


