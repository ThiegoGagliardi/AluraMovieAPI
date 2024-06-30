using System.ComponentModel.DataAnnotations;

namespace AluraMovieApi.Data.Dto;

public class UpdateMovieDto
{   

    [Required(ErrorMessage = "O título do filme é obrigatório")]
    [StringLength(80, ErrorMessage = "Excedido o número de caractéres para o título do filme")]
    public string Title { get; set; }

    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [StringLength(50, ErrorMessage = "Excedido o número de caracteres para o título do filme")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatória")]
    [Range(1, 600, ErrorMessage = "A duração deve ter entre 1 e 600 minutos")]
    public int Runtime { get; set; }
}