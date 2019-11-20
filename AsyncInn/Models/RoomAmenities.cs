using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class RoomAmenities
    {
        [Required]
        public int AmenitiesId { get; set; }
        [Required]
        public int RoomId { get; set; }

        // Navigation Properties
        public Amenities Amenities { get; set; }

        public Room Room { get; set; }
    }
}
