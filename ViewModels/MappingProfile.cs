using AutoMapper;
using MyrlandAAC.Models;

namespace myrlandaac.ViewModels
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>().ReverseMap();
        }
    }
}