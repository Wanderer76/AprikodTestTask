using Application.Dto;
using AprikodTestTask.Entities;
using AutoMapper;

namespace Application.Mappings
{
    public class GameMapping : Profile
    {
        public GameMapping()
        {
            CreateMap<Game, GameDto>()
                .ForMember(dto=>dto.Genres,opt=>opt.MapFrom(item=>item.Genres.Select(genre=>genre.Name)))
                .ReverseMap()
                .ForMember(dto=>dto.Genres,opt=>opt.Ignore());
                
            CreateMap<Genre, GenreDto>()
                .ForMember(dto => dto.Name, expression => expression.MapFrom(item => item.Name));
        }
    }
}
