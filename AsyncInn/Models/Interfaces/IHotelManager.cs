using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelManager
    {
        Task CreateHotelAsync(Hotel hotel);

        Task<Hotel> GetHotelAsync(int id);

        Task UpdateHotel(Hotel hotel);

        Task DeleteHotel(int id);
    }
}
