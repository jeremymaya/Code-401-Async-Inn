using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class HotelManager : IHotelManager
    {
        private AsyncInnDbContext _context;

        public HotelManager(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task CreateHotelAsync(Hotel hotel)
        {
            await _context.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            Hotel hotel = await GetHotelAsync(id);
            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotelAsync(int id) => await _context.Hotel.FirstOrDefaultAsync(hotel => hotel.ID == id);

        public async Task<IEnumerable<Hotel>> GetHotelsAsync() => await _context.Hotel.ToListAsync();

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            _context.Hotel.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<HotelRoom> GetHotelRoomsByHotel(int id)
        {
            var hotelRoom = _context.HotelRoom.Where(hotelRoom => hotelRoom.HotelId == id)
                .Include(x => x.Hotel)
                .Include(x => x.Room);
            return hotelRoom;
        }
    }
}
