using System.ComponentModel.DataAnnotations;


namespace AluraMovieApi.Data.Dto;

public class CreateMovieSessionDto
{
    public int MovieId { get; set; }

    public int TheaterId { get; set; } 

}
