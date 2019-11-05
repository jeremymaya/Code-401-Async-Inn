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

        public Task<List<Room>> GetRoomsAsync()
        {
            var rooms = _context.Room.ToListAsync();
            return rooms;
        }

        public async Task UpdateRoomAsync(Room room)
        {
            _context.Room.Update(room);
            await _context.SaveChangesAsync();
        }
    }
}
