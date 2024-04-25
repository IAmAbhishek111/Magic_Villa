using AutoMapper;
using Magic_Villa_WebApp.Models;
using Magic_Villa_WebApp.Models.Dto;
using Magic_Villa_WebApp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace Magic_Villa_WebApp.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;

            _mapper = mapper;

        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> list = new();

            /*      we need to use our villa service and call the method inside there.
                  We will say a variable response is equal to await.Inside the villa service we have get all async and the
                  API will always return the type which is API response. */

            var response = await _villaService.GetAllAsync<APIResponse>();


            //After we get the response back here, we will have to check if that is not null and a success is set to true. Then we will deserialize the object to a list of villa DTO. We will have to do convert.ToString. The response start result will have all the data.

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //The ValidateAntiForgeryToken attribute is used to prevent cross-site request forgery attacks. 
        public async Task<IActionResult> CreateVilla(CreateVillaDto model)
        {
            if (ModelState.IsValid)
            {
                // call the API
                var response = await _villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));

                return View(_mapper.Map<UpdateVillaDto>(model));
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //The ValidateAntiForgeryToken attribute is used to prevent cross-site request forgery attacks. 
        public async Task<IActionResult> UpdateVilla(UpdateVillaDto model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Villa updated successfully";
                // call the API
                var response = await _villaService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDto model)
        {

            var response = await _villaService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Error encountered.";

            return View(model);
        }
    }
}
