﻿using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Api.Models.Dto
{
	public class VillaNumberUpdateDto
	{
		[Required]
		public int VillaNo { get; set; }
		[Required]
		public int VillaID { get; set; }
		public string SpecialDetails { get; set; }
	}
}
