﻿using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Api.Models
{
    public class Villa
    {
        public int Id { get; set; }

        public string Name { get; set; }
    

        public DateTime CreatedDate { get; set; }
        
    }
}
