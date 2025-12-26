using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DashBourd.Models
{
    public class BookingViewModel
    {
        public int ShowtimeId { get; set; }
        public string MovieName { get; set; }
        public DateTime ShowDate { get; set; }
        public string CinemaName { get; set; }
        public decimal StandardPrice { get; set; } = 55; // SAR 55
        public decimal PremiumPrice { get; set; } = 65;  // SAR 65

        [Required(ErrorMessage = "الرجاء إدخال اسم العميل")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال رقم الهاتف")]
        public string CustomerPhone { get; set; }

        [Range(1, 10, ErrorMessage = "عدد التذاكر من 1 إلى 10")]
        public int Quantity { get; set; } = 1;

        public List<string> SelectedSeats { get; set; } = new List<string>(); // مثل ["A1", "B2"]

        // حساب الإجمالي بناءً على أنواع المقاعد
        public decimal TotalPrice
        {
            get
            {
                if (SelectedSeats == null || SelectedSeats.Count == 0) return 0;
                decimal total = 0;
                foreach (var seat in SelectedSeats)
                {
                    char row = seat[0]; // A, B, etc.
                    bool isPremium = (row >= 'A' && row <= 'E'); // Premium: A-E
                    total += isPremium ? PremiumPrice : StandardPrice;
                }
                return total;
            }
        }

        // للعرض: قائمة المقاعد المختارة
        public string SelectedSeatsDisplay => string.Join(", ", SelectedSeats);
    }

    // Model مساعد لخريطة المقاعد (للـ View)
    public class SeatMapModel
    {
        public string[,] Seats { get; set; } = new string[12, 6]; // 12 صف (0=L, 11=A), 6 أعمدة (1-6)
        public Dictionary<string, string> Status { get; set; } = new Dictionary<string, string>(); // "available", "booked", "selected", "disabled"
    }
}