namespace DashBourd.ViewModel
{
    public class LoginVM
    {
        public String UsernameOrEmail { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;
        public bool RememberMe { get; set; }
    }
}
