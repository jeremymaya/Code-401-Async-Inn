using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncInn.Migrations
{
    public partial class AddHotelSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "ID", "City", "Name", "Phone", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Seattle", "HotelOne", "206-681-1111", "WA", "2901 1st Ave" },
                    { 2, "Seattle", "HotelTwo", "206-681-2222", "WA", "2901 2nd Ave" },
                    { 3, "Seattle", "HotelThree", "206-681-3333", "WA", "2901 3rd Ave" },
                    { 4, "Seattle", "HotelFour", "206-681-4444", "WA", "2901 4th Ave" },
                    { 5, "Seattle", "HotelFive", "206-681-5555", "WA", "2901 5th Ave" },
                    { 6, "Seattle", "HotelSix", "206-681-6666", "WA", "2901 6th Ave" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "ID",
                keyValue: 6);
        }
    }
}
