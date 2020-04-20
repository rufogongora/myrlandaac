using AutoMapper;
using MyrlandAAC.Models;

namespace MyrlandAAC.ViewModels
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>().ReverseMap();
        }
    }
}