using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DashBourd.Migrations
{
    /// <inheritdoc />
    public partial class SeedCinemaSeatsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Image", "Location", "Name", "TotalSeats" },
                values: new object[] { 1, null, "القاهرة - مدينة نصر", "Andaloos", 110 });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "CinemaId", "IsDisabledAccess", "IsPremium", "Number", "Row", "SeatNumber" },
                values: new object[,]
                {
                    { 1, 1, false, false, 1, "L", "L1" },
                    { 2, 1, false, false, 2, "L", "L2" },
                    { 3, 1, false, false, 3, "L", "L3" },
                    { 4, 1, false, false, 4, "L", "L4" },
                    { 5, 1, false, false, 5, "L", "L5" },
                    { 6, 1, false, false, 6, "L", "L6" },
                    { 7, 1, false, false, 7, "L", "L7" },
                    { 8, 1, false, false, 8, "L", "L8" },
                    { 9, 1, false, false, 9, "L", "L9" },
                    { 10, 1, true, false, 10, "L", "L10" },
                    { 11, 1, false, false, 1, "K", "K1" },
                    { 12, 1, false, false, 2, "K", "K2" },
                    { 13, 1, false, false, 3, "K", "K3" },
                    { 14, 1, false, false, 4, "K", "K4" },
                    { 15, 1, false, false, 5, "K", "K5" },
                    { 16, 1, false, false, 6, "K", "K6" },
                    { 17, 1, false, false, 7, "K", "K7" },
                    { 18, 1, false, false, 8, "K", "K8" },
                    { 19, 1, false, false, 9, "K", "K9" },
                    { 20, 1, false, false, 10, "K", "K10" },
                    { 21, 1, false, false, 1, "J", "J1" },
                    { 22, 1, false, false, 2, "J", "J2" },
                    { 23, 1, false, false, 3, "J", "J3" },
                    { 24, 1, false, false, 4, "J", "J4" },
                    { 25, 1, false, false, 5, "J", "J5" },
                    { 26, 1, false, false, 6, "J", "J6" },
                    { 27, 1, false, false, 7, "J", "J7" },
                    { 28, 1, false, false, 8, "J", "J8" },
                    { 29, 1, false, false, 9, "J", "J9" },
                    { 30, 1, false, false, 10, "J", "J10" },
                    { 31, 1, false, false, 1, "I", "I1" },
                    { 32, 1, false, false, 2, "I", "I2" },
                    { 33, 1, false, false, 3, "I", "I3" },
                    { 34, 1, false, false, 4, "I", "I4" },
                    { 35, 1, false, false, 5, "I", "I5" },
                    { 36, 1, false, false, 6, "I", "I6" },
                    { 37, 1, false, false, 7, "I", "I7" },
                    { 38, 1, false, false, 8, "I", "I8" },
                    { 39, 1, false, false, 9, "I", "I9" },
                    { 40, 1, false, false, 10, "I", "I10" },
                    { 41, 1, false, false, 1, "H", "H1" },
                    { 42, 1, false, false, 2, "H", "H2" },
                    { 43, 1, false, false, 3, "H", "H3" },
                    { 44, 1, false, false, 4, "H", "H4" },
                    { 45, 1, false, false, 5, "H", "H5" },
                    { 46, 1, false, false, 6, "H", "H6" },
                    { 47, 1, false, false, 7, "H", "H7" },
                    { 48, 1, false, false, 8, "H", "H8" },
                    { 49, 1, false, false, 9, "H", "H9" },
                    { 50, 1, false, false, 10, "H", "H10" },
                    { 51, 1, false, false, 1, "G", "G1" },
                    { 52, 1, false, false, 2, "G", "G2" },
                    { 53, 1, false, false, 3, "G", "G3" },
                    { 54, 1, false, false, 4, "G", "G4" },
                    { 55, 1, false, false, 5, "G", "G5" },
                    { 56, 1, false, false, 6, "G", "G6" },
                    { 57, 1, false, false, 7, "G", "G7" },
                    { 58, 1, false, false, 8, "G", "G8" },
                    { 59, 1, false, false, 9, "G", "G9" },
                    { 60, 1, false, false, 10, "G", "G10" },
                    { 61, 1, false, false, 1, "F", "F1" },
                    { 62, 1, false, false, 2, "F", "F2" },
                    { 63, 1, false, false, 3, "F", "F3" },
                    { 64, 1, false, false, 4, "F", "F4" },
                    { 65, 1, false, false, 5, "F", "F5" },
                    { 66, 1, false, false, 6, "F", "F6" },
                    { 67, 1, false, false, 7, "F", "F7" },
                    { 68, 1, false, false, 8, "F", "F8" },
                    { 69, 1, false, false, 9, "F", "F9" },
                    { 70, 1, false, false, 10, "F", "F10" },
                    { 71, 1, false, true, 1, "E", "E1" },
                    { 72, 1, false, true, 2, "E", "E2" },
                    { 73, 1, false, true, 3, "E", "E3" },
                    { 74, 1, false, true, 4, "E", "E4" },
                    { 75, 1, false, true, 5, "E", "E5" },
                    { 76, 1, false, true, 6, "E", "E6" },
                    { 77, 1, false, true, 7, "E", "E7" },
                    { 78, 1, false, true, 8, "E", "E8" },
                    { 79, 1, false, true, 1, "D", "D1" },
                    { 80, 1, false, true, 2, "D", "D2" },
                    { 81, 1, false, true, 3, "D", "D3" },
                    { 82, 1, false, true, 4, "D", "D4" },
                    { 83, 1, false, true, 5, "D", "D5" },
                    { 84, 1, false, true, 6, "D", "D6" },
                    { 85, 1, false, true, 7, "D", "D7" },
                    { 86, 1, false, true, 8, "D", "D8" },
                    { 87, 1, false, true, 1, "C", "C1" },
                    { 88, 1, false, true, 2, "C", "C2" },
                    { 89, 1, false, true, 3, "C", "C3" },
                    { 90, 1, false, true, 4, "C", "C4" },
                    { 91, 1, false, true, 5, "C", "C5" },
                    { 92, 1, false, true, 6, "C", "C6" },
                    { 93, 1, false, true, 7, "C", "C7" },
                    { 94, 1, false, true, 8, "C", "C8" },
                    { 95, 1, false, true, 1, "B", "B1" },
                    { 96, 1, false, true, 2, "B", "B2" },
                    { 97, 1, false, true, 3, "B", "B3" },
                    { 98, 1, false, true, 4, "B", "B4" },
                    { 99, 1, false, true, 5, "B", "B5" },
                    { 100, 1, false, true, 6, "B", "B6" },
                    { 101, 1, false, true, 7, "B", "B7" },
                    { 102, 1, false, true, 8, "B", "B8" },
                    { 103, 1, false, true, 1, "A", "A1" },
                    { 104, 1, false, true, 2, "A", "A2" },
                    { 105, 1, false, true, 3, "A", "A3" },
                    { 106, 1, false, true, 4, "A", "A4" },
                    { 107, 1, false, true, 5, "A", "A5" },
                    { 108, 1, false, true, 6, "A", "A6" },
                    { 109, 1, false, true, 7, "A", "A7" },
                    { 110, 1, false, true, 8, "A", "A8" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
