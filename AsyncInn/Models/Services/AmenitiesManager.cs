using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class AmenitiesManager : IAmenitiesManager
    {
        private AsyncInnDbContext _context;

        public AmenitiesManager(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task CreateAmenityAsync(Amenities amenity)
        {
            await _context.AddAsync(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenityAsync(int id)
        {
            Amenities amenity = await GetAmenityAsync(id);
            _context.Remove(amenity);
            await _context.SaveChangesAsync();
        }

        public Task<List<Amenities>> GetAmenitiesAsync()
        {
            var amenities = _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenities> GetAmenityAsync(int id) => await _context.Amenities.FirstOrDefaultAsync(amenity => amenity.ID == id);
        
        public async Task UpdateAmenityAsync(Amenities amenity)
        {
            _context.Amenities.Update(amenity);
            await _context.SaveChangesAsync();
        }
    }
}
