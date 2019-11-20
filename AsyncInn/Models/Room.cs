﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Room
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Room Name")]
        public string Name { get; set; }
        [Required]
        public Layout Layout { get; set; }

        // Navigation Properties
        public ICollection<RoomAmenities> RoomAmenities { get; set; }

        public ICollection<HotelRoom> HotelRoom { get; set; }
    }

    public enum Layout
    {
        Studio,
        OneBedroom,
        TwoBedroom
    }
}
