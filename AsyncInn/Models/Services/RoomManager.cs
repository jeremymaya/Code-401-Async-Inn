using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomManager : IRoomManager
    {
        private AsyncInnDbContext _context;

        public RoomManager(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task CreateRoomAsync(Room room)
        {
            await _context.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(int id)
        {
            Room room = await GetRoomAsync(id);
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoomAsync(int id) => await _context.Room.FirstOrDefaultAsync(room => room.ID == id);

        public async Task<IEnumerable<Room>> GetRoomsAsync() => await _context.Room.ToListAsync();

        public async Task UpdateRoomAsync(Room room)
        {
            _context.Room.Update(room);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<RoomAmenities> GetRoomAmenitiesByRoom(int id)
        {
            var roomAmenities = _context.RoomAmenities.Where(roomAmenity => roomAmenity.RoomId == id)
                .Include(x => x.Room)
                .Include(x => x.Amenities);
            return roomAmenities;
        }
    }
}
