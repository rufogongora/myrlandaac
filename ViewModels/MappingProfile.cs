using AutoMapper;
using MyrlandAAC.Models;

namespace MyrlandAAC.ViewModels
{
    public class MappingProfile: Profile
    {
        private string[] Towns = new string[] { "Andicas", "Venoria" };
        public MappingProfile() {
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>()
            .ForMember(dest => dest.TownName, m => m.MapFrom(src => 
                Towns[src.Town - 1]
            ))
            .ReverseMap();
            
        }
    }
}