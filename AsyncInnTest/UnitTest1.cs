using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using static AsyncInn.Program;

namespace AsyncInnTest
{
    public class UnitTest1
    {
        [Fact]
        public void CanGetHotelName()
        {
            Hotel testHotel = new Hotel();
            testHotel.Name = "Test Hotel";

            Assert.Equal("Test Hotel", testHotel.Name);
        }

        [Fact]
        public void CanChangeHotelStreet()
        {
            Hotel testHotel = new Hotel();
            testHotel.Name = "Test Hotel";
            testHotel.StreetAddress = "0000 Test Street";
            testHotel.StreetAddress = "1234 Sesame Street";

            Assert.Equal("1234 Sesame Street", testHotel.StreetAddress);
        }

        [Fact]
        public void CanGetRoomName()
        {
            Room testRoom = new Room();
            testRoom.Name = "Test Room";

            Assert.Equal("Test Room", testRoom.Name);
        }

        [Fact]
        public void CanChangeRoomName()
        {
            Room testRoom = new Room();
            testRoom.Name = "Test Room";
            testRoom.Name = "Hello Room";

            Assert.Equal("Hello Room", testRoom.Name);
        }

        [Fact]
        public void CanGetRoomLayout()
        {
            Room testRoom = new Room();
            testRoom.Layout = Layout.OneBedroom;

            Assert.Equal(Layout.OneBedroom, testRoom.Layout);
        }

        [Fact]
        public void CanGetAmenityName()
        {
            Amenities testAmenity = new Amenities();
            testAmenity.Name = "Test Item";

            Assert.Equal("Test Item", testAmenity.Name);
        }

        [Fact]
        public void CanChangeAmenityName()
        {
            Amenities testAmenity = new Amenities();
            testAmenity.Name = "Test Item";
            testAmenity.Name = "Hello Item";

            Assert.Equal("Hello Item", testAmenity.Name);
        }

        [Fact]
        public void CanGetHotelRoomNumber()
        {
            HotelRoom testHotelRoom = new HotelRoom();
            testHotelRoom.RoomNumber = 000;

            Assert.Equal(000, testHotelRoom.RoomNumber);
        }

        [Fact]
        public void CanChangeRoomNumber()
        {
            HotelRoom testHotelRoom = new HotelRoom();
            testHotelRoom.RoomNumber = 000;
            testHotelRoom.RoomNumber = 999;

            Assert.Equal(999, testHotelRoom.RoomNumber);
        }

        [Fact]
        public async void SaveHotelInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingHotelInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                HotelManager service = new HotelManager(context);

                Hotel testHotel = new Hotel();
                testHotel.Name = "Test Hotel";
                testHotel.Phone = "000-000-0000";
                testHotel.StreetAddress = "0000 Test Stree";
                testHotel.State = "WA";

                await service.CreateHotelAsync(testHotel);

                Hotel result = await context.Hotel.FirstOrDefaultAsync(x => x.Name == testHotel.Name);

                Assert.Equal("Test Hotel", result.Name);
            }
        }

        [Fact]
        public async void UpdateHotelInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingHotelInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                HotelManager service = new HotelManager(context);

                Hotel testHotel = new Hotel();
                testHotel.Name = "Test Hotel";
                testHotel.Phone = "000-000-0000";
                testHotel.StreetAddress = "0000 Test Stree";
                testHotel.State = "WA";

                await service.CreateHotelAsync(testHotel);

                testHotel.Name = "Hello Hotel";
                await service.UpdateHotelAsync(testHotel);

                Hotel result = await context.Hotel.FirstOrDefaultAsync(x => x.Name == testHotel.Name);

                Assert.Equal("Hello Hotel", result.Name);
            }
        }

        [Fact]
        public async void DeleteHotelInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingHotelInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                HotelManager service = new HotelManager(context);

                Hotel testHotel = new Hotel();
                testHotel.ID = 2;
                testHotel.Name = "Test Hotel";
                testHotel.Phone = "000-000-0000";
                testHotel.StreetAddress = "0000 Test Stree";
                testHotel.State = "WA";

                await service.CreateHotelAsync(testHotel);

                await service.DeleteHotelAsync(2);

                Hotel result = await context.Hotel.FirstOrDefaultAsync(x => x.ID == testHotel.ID);

                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetHotelByIdInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingHotelInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                HotelManager service = new HotelManager(context);

                Hotel testHotel = new Hotel();
                testHotel.ID = 3;
                testHotel.Name = "Test Hotel";
                testHotel.Phone = "000-000-0000";
                testHotel.StreetAddress = "0000 Test Stree";
                testHotel.State = "WA";

                await service.CreateHotelAsync(testHotel);

                await service.GetHotelAsync(3);

                Hotel result = await context.Hotel.FirstOrDefaultAsync(x => x.Name == testHotel.Name);

                Assert.Equal("Test Hotel", result.Name);
            }
        }

        [Fact]
        public async void SaveRoomInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingRoomInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                RoomManager service = new RoomManager(context);

                Room testRoom = new Room();
                testRoom.Name = "Test Room";
                testRoom.Layout = Layout.OneBedroom;

                await service.CreateRoomAsync(testRoom);

                Room result = await context.Room.FirstOrDefaultAsync(x => x.Name == testRoom.Name);

                Assert.Equal("Test Room", result.Name);
            }
        }

        [Fact]
        public async void DeleteRoomInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingRoomInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                RoomManager service = new RoomManager(context);

                Room testRoom = new Room();
                testRoom.ID = 2;
                testRoom.Name = "Test Room";
                testRoom.Layout= Layout.OneBedroom;

                await service.DeleteRoomAsync(2);

                Room result = await context.Room.FirstOrDefaultAsync(x => x.ID == testRoom.ID);

                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetRoomByIdInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingRoomInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                RoomManager service = new RoomManager(context);

                Room testRoom = new Room();
                testRoom.ID = 3;
                testRoom.Name = "Test Room";
                testRoom.Layout = Layout.OneBedroom;

                await service.CreateRoomAsync(testRoom);

                await service.GetRoomAsync(3);

                Room result = await context.Room.FirstOrDefaultAsync(x => x.Name == testRoom.Name);

                Assert.Equal("Test Room", result.Name);
            }
        }

        [Fact]
        public async void UpdateRoomInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingRoomInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                RoomManager service = new RoomManager(context);

                Room testRoom = new Room();
                testRoom.Name = "Test Room";
                testRoom.Layout = Layout.OneBedroom;

                testRoom.Name = "Hello Room";
                await service.UpdateRoomAsync(testRoom);

                Room result = await context.Room.FirstOrDefaultAsync(x => x.Name == testRoom.Name);

                Assert.Equal("Hello Room", result.Name);
            }
        }

        [Fact]
        public async void SaveAmenityInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingAmenityInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                AmenitiesManager service = new AmenitiesManager(context);

                Amenities testAmenity= new Amenities();
                testAmenity.Name = "Test Amenity";
                
                await service.CreateAmenityAsync(testAmenity);

                Amenities result = await context.Amenities.FirstOrDefaultAsync(x => x.Name == testAmenity.Name);

                Assert.Equal("Test Amenity", result.Name);
            }
        }

        [Fact]
        public async void DeleteAmenityInDb()
        {
            DbContextOptions<AsyncInnDbContext> options = new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseInMemoryDatabase("SavingAmenityInDb")
                .Options;

            using (AsyncInnDbContext context = new AsyncInnDbContext(options))
            {
                AmenitiesManager service = new AmenitiesManager(context);

                Amenities testAmenity = new Amenities();
                testAmenity.Name = "Test Amenity";
                testAmenity.ID = 2;

                await service.DeleteAmenityAsync(2);

                Amenities result = await context.Amenities.FirstOrDefaultAsync(x => x.ID == testAmenity.ID);

                Assert.Null(result);
            }
        }
    }
}
