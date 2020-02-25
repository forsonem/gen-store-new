﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class Restaurant
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Style { get; set; }
        [Required]
        [Range(0d, 5d)]
        public int Rating { get; set; }
        [Required]
        [Range(1, 5)]
        public int DollarSigns { get; set; }
    }
}