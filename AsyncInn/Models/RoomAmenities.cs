using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class RoomAmenities
    {

        public int AmenitiesId { get; set; }
        public int RoomId { get; set; }

        // Navigation Properties
        public ICollection<Amenities> Amenities { get; set; }

        public ICollection<Room> Room { get; set; }
    }
}
