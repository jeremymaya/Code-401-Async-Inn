using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.ViewModels;

namespace AsyncInn.Controllers
{
    public class RoomsController : Controller
    {
        /// <summary>
        /// Dependency injection to establish a private connection to a database table by injecting an interface
        /// </summary>
        private readonly IRoomManager _room;

        /// <summary>
        /// A controller contructor to set controller's propety to the corresponding interface instance
        /// </summary>
        /// <param name="room">IRoomManager instance based on an attached database</param>
        public RoomsController(IRoomManager room)
        {
            _room = room;
        }

        // GET: Rooms
        /// <summary>
        /// Default HTTP GET route for /Rooms to display amenities in a database
        /// </summary>
        /// <returns>Index.cshtml with a rooms list</returns>
        public async Task<IActionResult> Index(string searchString)
        {
            var rooms = from x in await _room.GetRoomsAsync() select x;

            if (!string.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(x => x.Name.Contains(searchString));
            }
            return View(rooms);
        }

        // GET: Rooms/Details/5
        /// <summary>
        /// HTTP GET route for Rooms/Details to display room details
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Details.cshtml with a room details based on the Room Id including the amenities associated with the Room Id</returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Room room = await _room.GetRoomAsync(id);
            var roomAmenitiesList = _room.GetRoomAmenitiesByRoom(id);

            RoomAmenitiesListVM ravm = new RoomAmenitiesListVM();
            ravm.Room = room;
            ravm.RoomAmenities = roomAmenitiesList;

            if (room == null)
            {
                return NotFound();
            }

            return View(ravm);
        }

        // GET: Rooms/Create
        /// <summary>
        /// HTTP GET route for Rooms/Create to get a new room information
        /// </summary>
        /// <returns>Create.cshtml</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// HTTP POST route for Rooms/Create to create a new room
        /// </summary>
        /// <param name="room">New room object</param>
        /// <returns>Index.cshtml with the updated rooms list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Layout")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _room.CreateRoomAsync(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        /// <summary>
        /// HTTP GET route for Rooms/Edit to get a room information based on the Room Id
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Edit.cshtml with a room information based on the Room Id</returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var room = await _room.GetRoomAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// HTTP POST route for Rooms/Edit/ to edit a room  details
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <param name="room">Room object based on the Room Id</param>
        /// <returns>Index.cshtml with the updated rooms list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Layout")] Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _room.UpdateRoomAsync(room);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RoomExists(room.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        /// <summary>
        /// HTTP GET route for Rooms/Delete/ to get a room to be deleted based on the Room Id
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Delete.cshtml with a room information based on the Room Id</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var room = await _room.GetRoomAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        /// <summary>
        /// HTTP POST route for Rooms/Delete/ to delete a room based on the Room Id
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Index.cshtml with the updated room list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _room.DeleteRoomAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// An action that checks if a room exists based on the Room Id
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Boolean value which confirms if a room  based on the Room Id exists</returns>
        private async Task<bool> RoomExists(int id)
        {
            var room = await _room.GetRoomAsync(id);
            if (room != null)
            {
                return true;
            }

            return false;
        }
    }
}
