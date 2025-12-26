using AutoMapper;
using QuickFun.Application.DTOs;
using QuickFun.Domain.Entities;

namespace QuickFun.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Game mappings
        CreateMap<Game, GameDto>().ReverseMap();

        // Player mappings
        CreateMap<Player, PlayerDto>()
            .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.PlayerName.Value))
            .ReverseMap()
            .ForMember(dest => dest.PlayerName, opt => opt.Ignore());

        // Score mappings
        CreateMap<Score, ScoreDto>()
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Value.Points))
            .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.PlayerName.Value))
            .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Name))
            .ReverseMap()
            .ForMember(dest => dest.Value, opt => opt.Ignore())
            .ForMember(dest => dest.Player, opt => opt.Ignore())
            .ForMember(dest => dest.Game, opt => opt.Ignore());
    }
}