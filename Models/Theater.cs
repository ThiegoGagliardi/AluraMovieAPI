using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AluraMovieApi.Models;

public class Theater
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Name { get; set; }

    public int AdressId { get; set; }

    private Adress _adress;

    private List<MovieSession>? _movieSessions;

    private ILazyLoader LazyLoader { get; set; }

    public Theater(ILazyLoader lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
    public Theater()
    {
        
    }


    public virtual Adress Adress
    {
        get => LazyLoader.Load(this, ref _adress);
        set => _adress = value;
    }        

    public virtual List<MovieSession> MovieSessions
    {
        get => LazyLoader.Load(this, ref _movieSessions);
        set => _movieSessions = value;
    }   
}