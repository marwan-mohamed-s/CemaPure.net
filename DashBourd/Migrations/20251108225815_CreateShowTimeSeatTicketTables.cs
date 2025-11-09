using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBourd.Migrations
{
    /// <inheritdoc />
    public partial class CreateShowTimeSeatTicketTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Cinemas_CinemaId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtime_Cinemas_CinemaId",
                table: "Showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtime_Movies_MovieId",
                table: "Showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_UserId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Showtime_ShowtimeId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtime",
                table: "Showtime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Showtime",
                newName: "Showtimes");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "Seats");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UserId",
                table: "Tickets",
                newName: "IX_Tickets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_ShowtimeId_SeatId",
                table: "Tickets",
                newName: "IX_Tickets_ShowtimeId_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_SeatId",
                table: "Tickets",
                newName: "IX_Tickets_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtime_MovieId",
                table: "Showtimes",
                newName: "IX_Showtimes_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtime_CinemaId",
                table: "Showtimes",
                newName: "IX_Showtimes_CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_CinemaId",
                table: "Seats",
                newName: "IX_Seats_CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Cinemas_CinemaId",
                table: "Seats",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Cinemas_CinemaId",
                table: "Showtimes",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatId",
                table: "Tickets",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId",
                principalTable: "Showtimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Cinemas_CinemaId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Cinemas_CinemaId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_UserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Showtimes",
                newName: "Showtime");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "Seat");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_UserId",
                table: "Ticket",
                newName: "IX_Ticket_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ShowtimeId_SeatId",
                table: "Ticket",
                newName: "IX_Ticket_ShowtimeId_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SeatId",
                table: "Ticket",
                newName: "IX_Ticket_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtime",
                newName: "IX_Showtime_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_CinemaId",
                table: "Showtime",
                newName: "IX_Showtime_CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_CinemaId",
                table: "Seat",
                newName: "IX_Seat_CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtime",
                table: "Showtime",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Cinemas_CinemaId",
                table: "Seat",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtime_Cinemas_CinemaId",
                table: "Showtime",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtime_Movies_MovieId",
                table: "Showtime",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_UserId",
                table: "Ticket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Showtime_ShowtimeId",
                table: "Ticket",
                column: "ShowtimeId",
                principalTable: "Showtime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
