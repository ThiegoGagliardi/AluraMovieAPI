using AluraMovieApi.Models;
using AluraMovieApi.Data.Dto;

namespace AluraMovieApi.Profile;

public class MovieProfile : AutoMapper.Profile
{
    public MovieProfile(){
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();
        CreateMap<Movie, UpdateMovieDto>();
        CreateMap<Movie, ReadMovieDto>().ForMember(movieDto => movieDto.MovieSessions, 
                                                   opts => opts.MapFrom(movie => movie.MovieSessions));
    }
}
