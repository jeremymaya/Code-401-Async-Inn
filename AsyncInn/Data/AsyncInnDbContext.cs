using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {

        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomAmenities>().HasKey(roomamenities => new { roomamenities.AmenitiesId, roomamenities.RoomId });
            modelBuilder.Entity<HotelRoom>().HasKey(hotelroom => new { hotelroom.HotelId, hotelroom.RoomId });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    ID = 1,
                    Name = "HotelOne",
                    StreetAddress = "2901 1st Ave",
                    City = "Seattle",
                    State = "WA",
                    Phone = "206-681-1111"
                },
                new Hotel
                {
                    ID = 2,
                    Name = "HotelTwo",
                    StreetAddress = "2901 2nd Ave",
                    City = "Seattle",
                    State = "WA",
                    Phone = "206-681-2222"
                },
                new Hotel
                {
                    ID = 3,
                    Name = "HotelThree",
                    StreetAddress = "2901 3rd Ave",
                    City = "Seattle",
                    State = "WA",
                    Phone = "206-681-3333"
                },
                new Hotel
                {
                    ID = 4,
                    Name = "HotelFour",
                    StreetAddress = "2901 4th Ave",
                    City = "Seattle",
                    State = "WA",
                    Phone = "206-681-4444"
                },
                new Hotel
                {
                    ID = 5,
                    Name = "HotelFive",
                    StreetAddress = "2901 5th Ave",
                    City = "Seattle",
                    State = "WA",
                    Phone = "206-681-5555"
                },
                new Hotel
                {
                    ID = 6,
                    Name = "HotelSix",
                    StreetAddress = "2901 6th Ave",
                    City = "Seattle",
                    State = "WA",
                    Phone = "206-681-6666"
                }
                );
        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<HotelRoom> HotelRoom { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
    }
}
