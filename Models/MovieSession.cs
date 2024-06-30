using System.ComponentModel.DataAnnotations;

namespace AluraMovieApi.Models;

public class MovieSession
{
    public int MovieId { get; set; }

    public int TheaterId { get; set; }

    public virtual Movie Movie { get; set; }

    public virtual Theater Theater { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

}
