using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.ViewModels
{
    public class HotelRoomsListVM
    {
        public Hotel Hotel { get; set; }

        public IEnumerable<HotelRoom> HotelRooms { get; set; }
    }
}
