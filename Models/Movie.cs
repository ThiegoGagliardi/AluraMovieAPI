using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AluraMovieApi.Models;
public class Movie
{
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    [MaxLength(80, ErrorMessage = "Excedido o número de caractéres para o título do filme")]
    public string Title { get; set; }

   [Required(ErrorMessage = "O gênero do filme é obrigatório")]
   [MaxLength(50, ErrorMessage = "Excedido o número de caracteres para o título do filme")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatória")]
    [Range(1,600, ErrorMessage ="A duração deve ter entre 1 e 600 minutos")]
    public int Runtime { get; set; }

    private List<MovieSession> _movieSessions;

    private ILazyLoader LazyLoader { get; set; }

    public Movie(ILazyLoader lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    public Movie()
    {
       
    }

    public virtual List<MovieSession> MovieSessions
    {
        get => LazyLoader.Load(this, ref _movieSessions);
        set => _movieSessions = value;
    }              

}
