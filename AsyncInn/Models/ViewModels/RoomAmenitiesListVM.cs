using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.ViewModels
{
    public class RoomAmenitiesListVM
    {
        public Room Room { get; set; }

        public IEnumerable<RoomAmenities> RoomAmenities { get; set; }
    }
}
