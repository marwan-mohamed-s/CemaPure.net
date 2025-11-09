namespace DashBourd.ViewModel
{
    public class BookTicketViewModel
    {
        public int ShowtimeId { get; set; }
        public string MovieName { get; set; }
        public string CinemaName { get; set; }
        public DateTime ShowDateTime { get; set; }
        public decimal StandardPrice { get; set; }
        public decimal PremiumPrice { get; set; }

        public List<SeatViewModel> Seats { get; set; } = new();
        public List<int> TakenSeatIds { get; set; } = new();
    }

}
