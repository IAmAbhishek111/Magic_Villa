﻿using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Api.Models.Dto
{
    public class UpdateVillaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]

        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]

        public int Occupancy { get; set; }
        [Required]

        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
