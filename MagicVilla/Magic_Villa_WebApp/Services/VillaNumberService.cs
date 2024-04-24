using Magic_Villa_WebApp.Models;
using Magic_Villa_WebApp.Models.Dto;
using Magic_Villa_WebApp.Services.IServices;
using MagicVilla_Utility;
using System;

namespace Magic_Villa_WebApp.Services
{
	public class VillaNumberService : BaseService, IVillaNumberService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string villaUrl;

		public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

		}

		public Task<T> CreateAsync<T>(VillaNumberCreateDto dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = villaUrl + "/api/villaNumberAPI"
			});
		}



		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = villaUrl + "/api/VillaNumberAPI/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/VillaNumberAPI"
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/VillaNumberAPI/" + id
			});
		}

		public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = villaUrl + "/api/VillaNumberAPI/" + dto.VillaNo
			});
		}
	}
}

