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

namespace AsyncInn.Controllers
{
    public class AmenitiesController : Controller
    {
        /// <summary>
        /// Dependency injection to establish a private connection to a database table by injecting an interface
        /// </summary>
        private readonly IAmenitiesManager _amenities;

        /// <summary>
        /// A controller contructor to set controller's propety to the corresponding interface instance
        /// </summary>
        /// <param name="amenities">IAmenitiesManager instance based on an attached database</param>
        public AmenitiesController(IAmenitiesManager amenities)
        {
            _amenities = amenities;
        }

        // GET: Amenities
        /// <summary>
        /// Default HTTP GET route for /Amenities to display amenities in a database
        /// </summary>
        /// <returns>Index.cshtml with a amenities list</returns>
        public async Task<IActionResult> Index(string searchString)
        {
            var amenities = from x in await _amenities.GetAmenitiesAsync() select x;

            if (!string.IsNullOrEmpty(searchString))
            {
                amenities = amenities.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
            }

            return View(amenities);
        }

        // GET: Amenities/Details/5
        /// <summary>
        /// HTTP GET route for Amenities/Details to display amenity details
        /// </summary>
        /// <param name="id">Amenity Id</param>
        /// <returns>Details.cshtml with an amenity details based on the Amenity Id</returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var amenities = await _amenities.GetAmenityAsync(id);
            if (amenities == null)
            {
                return NotFound();
            }

            return View(amenities);
        }

        /// <summary>
        /// HTTP GET route for Amenities/Create to get a new amenity information
        /// </summary>
        /// <returns>Create.cshtml</returns>
        // GET: Amenities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amenities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// HTTP POST route for Amenities/Create to create a new amenity
        /// </summary>
        /// <param name="amenities">New amenity object</param>
        /// <returns>Index.cshtml with the updated amenities list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Amenities amenities)
        {
            if (ModelState.IsValid)
            {
                await _amenities.CreateAmenityAsync(amenities);
                return RedirectToAction(nameof(Index));
            }
            return View(amenities);
        }

        // GET: Amenities/Edit/5
        /// <summary>
        /// HTTP GET route for Amenities/Edit to get an amenity information based on the Amenity Id
        /// </summary>
        /// <param name="id">Amenity Id</param>
        /// <returns>Edit.cshtml with an amenity information based on the Amenity Id</returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var amenities = await _amenities.GetAmenityAsync(id);
            if (amenities == null)
            {
                return NotFound();
            }
            return View(amenities);
        }

        // POST: Amenities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// HTTP POST route for Amenities/Edit/ to edit an ameinity details
        /// </summary>
        /// <param name="id">Amenity Id</param>
        /// <param name="amenities">Amenity object based on the Amenity Id</param>
        /// <returns>Index.cshtml with the updated amenities list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Amenities amenities)
        {
            if (id != amenities.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _amenities.UpdateAmenityAsync(amenities);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AmenitiesExists(amenities.ID))
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
            return View(amenities);
        }

        // GET: Amenities/Delete/5
        /// <summary>
        /// HTTP GET route for Amenities/Delete/ to get an amenity to be deleted based on the Amenity Id
        /// </summary>
        /// <param name="id">Amenity Id</param>
        /// <returns>Delete.cshtml with an amenity information based on the Amenity Id</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var amenities = await _amenities.GetAmenityAsync(id);
            if (amenities == null)
            {
                return NotFound();
            }

            return View(amenities);
        }

        // POST: Amenities/Delete/5
        /// <summary>
        /// HTTP POST route for Amenities/Delete/ to delete an amenity based on the Amenity Id
        /// </summary>
        /// <param name="id">Amenity id</param>
        /// <returns>Index.cshtml with the updated amenities list</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _amenities.DeleteAmenityAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// An action that checks if an amenity exists based on the Amenity Id
        /// </summary>
        /// <param name="id">Amenity Id</param>
        /// <returns>Boolean value which confirms if an amenity  based on the Amenity Id exists</returns>
        private async Task<bool> AmenitiesExists(int id)
        {
            var amenity = await _amenities.GetAmenityAsync(id);
            if (amenity != null)
            {
                return true;
            }

            return false;
        }
    }
}
