namespace DashBourd.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string SeatNumber { get; set; } // مثل A1, B5
        public DateTime BookingDate { get; set; }

        public Movie Movie { get; set; }
        public ApplicationUser User { get; set; }
    }
}
