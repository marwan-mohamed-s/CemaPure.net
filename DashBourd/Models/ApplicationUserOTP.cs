namespace DashBourd.Models
{
    public class ApplicationUserOTP
    {
        public String Id { get; set; }
        public String OTP { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsValid { get; set; }

        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
