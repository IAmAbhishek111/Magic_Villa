﻿using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Api.Models.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }

        public VillaDto Villa { get; set; }
    }
}
