using AutoMapper;
using Magnum_PPT.Dto;
using Magnum_PPT.Entities;

namespace Magnum_PPT.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerDTO>().ReverseMap();
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<Round, RoundDTO>().ReverseMap();
        }
    }
}
