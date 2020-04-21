using MyrlandAAC.Enums;

namespace MyrlandAAC.ViewModels
{
    public class LoginResponseViewModel
    {
        public AccountViewModel Account { get; set; }
        public string Token { get; set; }
        public LoginResponseEnum Response { get; set; }
        public RoleEnum Role { get; set; }

    }
}