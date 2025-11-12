namespace DashBourd.Models
{
    public class PendingBooking
    {
        public int Id { get; set; }
        public string CheckoutSessionId { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string Seats { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
