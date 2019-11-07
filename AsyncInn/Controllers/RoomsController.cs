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
        private readonly IRoomManager _room;

        public RoomsController(IRoomManager room)
        {
            _room = room;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _room.GetRoomsAsync());
        }

        // GET: Rooms/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _room.DeleteRoomAsync(id);
            return RedirectToAction(nameof(Index));
        }

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
