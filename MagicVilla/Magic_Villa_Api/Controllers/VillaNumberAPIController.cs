﻿using Magic_Villa_Api.Data;
using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Magic_Villa_Api.Repository.IRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;
/*using Magic_Villa_Api.Logging;*//**/

namespace Magic_Villa_Api.Controllers
{
	[Route("api/VillaNumberAPI")]
	[ApiController]
	public class VillaNumberAPIController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IVillaNumberRepository _dbVillaNumber;
		private readonly IVillaRepository _dbVilla;
		private readonly IMapper _mapper;
		public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper,
			IVillaRepository dbVilla)
		{
			_dbVillaNumber = dbVillaNumber;
			_mapper = mapper;
			_dbVilla = dbVilla;

			this._response = new();
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetVillaNumbers()
		{
			try
			{

				IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync(includeProperties: "Villa");
				_response.Result = _mapper.Map<List<VillaNumberDto>>(villaNumberList);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);

			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;

		}

		[HttpGet("{id:int}", Name = "GetVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
				if (villaNumber == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				_response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}


        [Authorize(Roles = "admin")]
        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDTO)
		{
			try
			{

				if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null)
				{
					ModelState.AddModelError("ErrorMessages", "Villa Number already Exists!");
					return BadRequest(ModelState);
				}
				if (await _dbVilla.GetAsync(u => u.Id == createDTO.VillaID) == null)
				{
					ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
					return BadRequest(ModelState);
				}

				if (createDTO == null)
				{
					return BadRequest(createDTO);
				}

				VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);


				await _dbVillaNumber.CreateAsync(villaNumber);
				_response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetVilla", new { id = villaNumber.VillaNo }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}
        [Authorize(Roles = "admin")]

        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
		public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
		{
			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
				if (villaNumber == null)
				{
					return NotFound();
				}
				await _dbVillaNumber.RemoveAsync(villaNumber);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}

        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto updateDTO)
		{
			try
			{
				if (updateDTO == null || id != updateDTO.VillaNo)
				{
					return BadRequest();
				}
				if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaID) == null)
				{
					ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
					return BadRequest(ModelState);
				}

				VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

				await _dbVillaNumber.UpdateAsync(model);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}
	}
}
