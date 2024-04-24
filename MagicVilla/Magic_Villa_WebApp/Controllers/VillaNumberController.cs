using AutoMapper;
using Magic_Villa_WebApp.Models;
using Magic_Villa_WebApp.Models.Dto;
using Magic_Villa_WebApp.Services;
using Magic_Villa_WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace Magic_Villa_WebApp.Controllers
{
    public class VillaNumberController : Controller
    {
		private readonly IVillaNumberService _villaNumberService;
		private readonly IMapper _mapper;
		public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper)
        {
			_villaNumberService = villaNumberService;
			_mapper = mapper;
		}

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> list = new();

            /*      we need to use our villa service and call the method inside there.
                  We will say a variable response is equal to await.Inside the villa service we have get all async and the
                  API will always return the type which is API response. */

            var response = await _villaNumberService.GetAllAsync<APIResponse>();


            //After we get the response back here, we will have to check if that is not null and a success is set to true. Then we will deserialize the object to a list of villa DTO. We will have to do convert.ToString. The response start result will have all the data.

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

    }
}
