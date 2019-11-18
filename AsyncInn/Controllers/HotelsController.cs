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
    public class HotelsController : Controller
    {
        /// <summary>
        /// Dependency injection to establish a private connection to a database table by injecting an interface
        /// </summary>
        private readonly IHotelManager _hotel;

        /// <summary>
        /// A controller contructor to set controller's propety to the corresponding interface instance
        /// </summary>
        /// <param name="hotel">IHotelManager instance based on an attached database</param>
        public HotelsController(IHotelManager hotel)
        {
            _hotel = hotel;
        }

        // GET: Hotels
        /// <summary>
        /// Default HTTP GET route for /Hotels to display hotels in a database
        /// </summary>
        /// <returns>Index.cshtml with a hotels list</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _hotel.GetHotelsAsync());
        }

        // GET: Hotels/Details/5
        /// <summary>
        /// HTTP GET route for Hotels/Details/ to display a hotel details 
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>Details.cshtml with a hotel details based on the Hotel Id including the rooms associated with the Hotel Id</returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Hotel hotel = await _hotel.GetHotelAsync(id);
            var hotelRoomsList = _hotel.GetHotelRoomsByHotel(id);

            HotelRoomsListVM hrvm = new HotelRoomsListVM();
            hrvm.Hotel = hotel;
            hrvm.HotelRooms = hotelRoomsList;
            
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hrvm);
        }

        /// <summary>
        /// HTTP GET route for Hotels/Create to get a new hotel information
        /// </summary>
        /// <returns>Create.cshtml</returns>
        // GET: Hotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// HTTP POST route doe Hotels/Create to create a new hotel
        /// </summary>
        /// <param name="hotel">New hotel object</param>
        /// <returns>Index.cshtml with the updated hotels list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,StreetAddress,City,State,Phone")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _hotel.CreateHotelAsync(hotel);
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        /// <summary>
        /// HTTP GET route for Amenities/Edit to get a hotel information based on the Hotel Id
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>Edit.cshtml with a hotel infomration based on the Hotel Id</returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var hotel = await _hotel.GetHotelAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// HTTP POST route for Hotels/Edit/ to edit an hotel  details
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <param name="hotel">Hotel object based on the Hotel Id</param>
        /// <returns>Index.cshtml with the updated hotels list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,StreetAddress,City,State,Phone")] Hotel hotel)
        {
            if (id != hotel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _hotel.UpdateHotelAsync(hotel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HotelExists(hotel.ID))
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
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        /// <summary>
        /// HTTP GET route for Hotels/Delete/ to get a hotel to be deleted based on the Hotel Id
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>Delete.cshtml with a hotel information based on the Hotel Id</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var hotel = await _hotel.GetHotelAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        /// <summary>
        /// HTTP POST route for Hotels/Delete/ to delete a hotel based on the Hotel Id
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>Index.cshtml with the updated hotels list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            await _hotel.DeleteHotelAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// An action that checks if a hotel exists based on the Hotel Id
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>Boolean value which confirms if a hotel based on the Hotel Id exists</returns>
        private async Task<bool> HotelExists(int id)
        {
            var hotel = await _hotel.GetHotelAsync(id);
            if (hotel != null)
            {
                return true;
            }

            return false;
        }
    }
}
