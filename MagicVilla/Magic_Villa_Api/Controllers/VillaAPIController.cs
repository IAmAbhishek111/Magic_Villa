using Magic_Villa_Api.Data;
using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Magic_Villa_Api.Logging;

namespace Magic_Villa_Api.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase

    {
        //Now the main question is how do we log inside our VillaAPI controller?In order to log here, we need to use dependency injection because like I said before, logging is already registered in our application. So whenever we need an implementation, we just need to ask for an implementation of logger and that will be done inside constructor.

        private readonly ILogging  _logger;
        public VillaAPIController(ILogging logger)
        {
            _logger =  logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {

            _logger.Log("Get All Villas !" , "");


            // ok object created the status code OK 200
            return Ok(VillaStore.VillaList);


        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        // for documented :

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<VillaDto> GetVilla(int id)
        {

            

            if (id == 0)
            {
                _logger.Log("Get error in Villa " + id , "error");

                return BadRequest(); // give status code : 400

            }

            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound(); // give status code : 404
            }

            // ok object created the status code OK 200
            return Ok(villa);


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);

            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //Then the villa ID that we have so far in our villa store, we just have one and two so we can retrieve the maximum ID here and increment that by one and assign that to VillaDTO.

            villaDTO.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            VillaStore.VillaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }

            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa  == null)
            {
                return NotFound();
            }

            VillaStore.VillaList.Remove(villa);

            return NoContent();




        }



        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id , JsonPatchDocument<VillaDto> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
             var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

            if(villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();


        }

    }
}
