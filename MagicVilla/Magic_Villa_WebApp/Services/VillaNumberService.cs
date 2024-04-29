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

		public Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = villaUrl + "/api/villaNumberAPI",
                Token = token
            });
		}



		public Task<T> DeleteAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = villaUrl + "/api/VillaNumberAPI/" + id,
                Token = token
            });
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/VillaNumberAPI",
                Token = token
            });
		}

		public Task<T> GetAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/VillaNumberAPI/" + id,
                Token = token
            });
		}

		public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = villaUrl + "/api/VillaNumberAPI/" + dto.VillaNo,
                Token = token
            });
		}
	}
}

