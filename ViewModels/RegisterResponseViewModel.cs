using MyrlandAAC.Enums;

namespace MyrlandAAC.ViewModels
{
    public class RegisterResponseViewModel
    {
        public AccountViewModel Account {get;set;}
        public string Token {get;set;}
        public RegisterAccountResponseEnum Response {get;set;}

    }
}