using AluraMovieApi.Models;
using AluraMovieApi.Data.Dto;

using AutoMapper;

namespace AluraMovieApi.Profile;

public class TheaterProfile : AutoMapper.Profile
{
    public TheaterProfile()
    {
        CreateMap<CreateTheaterDto, Theater>();
        CreateMap<UpdateTheaterDto, Theater>();
        CreateMap<Theater, UpdateTheaterDto>();
        CreateMap<Theater, ReadTheaterDto>()
                                 .ForMember(theaterdto => theaterdto.Adress, 
                                             opts => opts.MapFrom(theater => theater.Adress))
                                 .ForMember(theaterdto => theaterdto.MovieSession, 
                                             opts => opts.MapFrom(movie => movie.MovieSessions));
    }
}
