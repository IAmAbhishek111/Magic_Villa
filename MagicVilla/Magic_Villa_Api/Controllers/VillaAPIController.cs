using Magic_Villa_Api.Data;
using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
/*using Magic_Villa_Api.Logging;*//**/

namespace Magic_Villa_Api.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase

    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;


        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            /*
                        _logger.Log("Get All Villas !" , "");*/
            // ok object created the status code OK 200

            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villaList));

        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        // for documented :

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {/*
                _logger.Log("Get error in Villa " + id , "error");*/

                return BadRequest(); // give status code : 400

            }

            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound(); // give status code : 404
            }

            // ok object created the status code OK 200
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] CreateVillaDto createDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }

            if (createDTO == null)
            {
                return BadRequest(createDTO);

            }
            /*    if (villaDTO.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }*/

            //Then the villa ID that we have so far in our villa store, we just have one and two so we can retrieve the maximum ID here and increment that by one and assign that to VillaDTO.
            /*
                        villaDTO.Id = _db.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            */

            Villa model = _mapper.Map<Villa>(createDTO);
           /* Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                *//*Id = villaDTO.Id,*//*
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };*/

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();


            return NoContent();
        }



        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] UpdateVillaDto updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }
            /*  var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
              villa.Name = villaDTO.Name;
              villa.Sqft = villaDTO.Sqft;
              villa.Occupancy = villaDTO.Occupancy;*/
            /*
                        Villa model = new()
                        {
                            Amenity = villaDTO.Amenity,
                            Details = villaDTO.Details,
                            Id = villaDTO.Id,
                            ImageUrl = villaDTO.ImageUrl,
                            Name = villaDTO.Name,
                            Occupancy = villaDTO.Occupancy,
                            Rate = villaDTO.Rate,
                            Sqft = villaDTO.Sqft
                        };*/

            Villa model = _mapper.Map<Villa>(updateDTO);
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<UpdateVillaDto> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            /*   villa.Name = "New Name";
               _db.SaveChanges();
   */

            /*   UpdateVillaDto villaDTO = new()
               {
                   Amenity = villa.Amenity,
                   Details = villa.Details,
                   Id = villa.Id,
                   ImageUrl = villa.ImageUrl,
                   Name = villa.Name,
                   Occupancy = villa.Occupancy,
                   Rate = villa.Rate,
                   Sqft = villa.Sqft
               };*/

            UpdateVillaDto villaDTO = _mapper.Map<UpdateVillaDto>(villa);

            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO, ModelState);
            /* Villa model = new Villa()
             {
                 Amenity = villaDTO.Amenity,
                 Details = villaDTO.Details,
                 Id = villaDTO.Id,
                 ImageUrl = villaDTO.ImageUrl,
                 Name = villaDTO.Name,
                 Occupancy = villaDTO.Occupancy,
                 Rate = villaDTO.Rate,
                 Sqft = villaDTO.Sqft
             };
 */
            Villa model = _mapper.Map<Villa>(villaDTO);
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }

    }
}
