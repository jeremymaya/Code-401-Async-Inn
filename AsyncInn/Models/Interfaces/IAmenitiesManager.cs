using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenitiesManager
    {
        Task CreateAmenityAsync(Amenities amenity);

        Task<Amenities> GetAmenityAsync(int id);

        Task UpdateAmenityAsync(Amenities amenity);

        Task DeleteAmenityAsync(int id);

        Task<IEnumerable<Amenities>> GetAmenitiesAsync();

        IEnumerable<RoomAmenities> GetRoomAmenitiesByAmenity(int amenitiesId);
    }
}
