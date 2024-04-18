using Magic_Villa_Api.Data;
using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa_Api.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase

    {

        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {

            return VillaStore.VillaList;


        }
        [HttpGet("id")]
        public VillaDto GetVilla(int id )
        {

            return VillaStore.VillaList.FirstOrDefault(u=> u.Id == id);


        }
    }
}
