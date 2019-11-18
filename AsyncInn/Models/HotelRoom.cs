using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class HotelRoom
    {
        public int HotelId { get; set; }
        [Required]
        [DisplayName("Room Number")]
        public int RoomNumber { get; set; }
        [Required]
        public int RoomId { get; set; }
        public decimal Rate { get; set; }
        [DisplayName("Is the room pet firendly?")]
        public bool PetFriendly { get; set; }

        // Navigation Properties
        public Hotel Hotel { get; set; }

        public Room Room { get; set; }
    }
}
