using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBourd.Migrations
{
    public partial class AddPendingBookingsOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // === إضافة جدول PendingBookings فقط ===
            migrationBuilder.CreateTable(
                name: "PendingBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutSessionId = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Seats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingBookings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingBookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // === إضافة الـ Indexes ===
            migrationBuilder.CreateIndex(
                name: "IX_PendingBookings_CheckoutSessionId",
                table: "PendingBookings",
                column: "CheckoutSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBookings_MovieId",
                table: "PendingBookings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingBookings_UserId",
                table: "PendingBookings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PendingBookings");
        }
    }
}