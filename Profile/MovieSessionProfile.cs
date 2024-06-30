using AluraMovieApi.Models;
using AluraMovieApi.Data.Dto;

namespace AluraMovieApi.Profile;

public class MovieSessionProfile : AutoMapper.Profile
{
    public MovieSessionProfile(){
        CreateMap<CreateMovieSessionDto, MovieSession>();
        CreateMap<MovieSession, ReadMovieSessionDto>();
    }
}