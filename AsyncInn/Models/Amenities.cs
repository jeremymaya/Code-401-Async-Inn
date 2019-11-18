﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Amenities
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Amenity Name")]
        public string Name { get; set; }

        // Navigation Properties
        public ICollection<RoomAmenities> RoomAmenities { get; set; }
    }
}
