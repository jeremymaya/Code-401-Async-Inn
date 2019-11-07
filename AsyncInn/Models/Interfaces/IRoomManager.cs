using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoomManager
    {
        Task CreateRoomAsync(Room room);

        Task<Room> GetRoomAsync(int id);

        Task UpdateRoomAsync(Room room);

        Task DeleteRoomAsync(int id);

        Task<IEnumerable<Room>> GetRoomsAsync();

        IEnumerable<RoomAmenities> GetRoomAmenitiesByRoom(int id);
    }
}
