using System.ComponentModel.DataAnnotations;

namespace AluraMovieApi.Data.Dto;

public class CreateTheaterDto
{

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Name { get; set; } = String.Empty;

    public int AdressId { get; set;}

}
