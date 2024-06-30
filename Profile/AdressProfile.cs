using AluraMovieApi.Models;
using AluraMovieApi.Data.Dto;

namespace AluraMovieApi.Profile;

public class AdressProfile : AutoMapper.Profile
{
    public AdressProfile()
    {
        CreateMap<CreateAdressDto, Adress>();
        CreateMap<UpdateAdressDto, Adress>();
        CreateMap<Adress, UpdateAdressDto>();
        CreateMap<Adress, ReadAdressDto>();        
    }
}