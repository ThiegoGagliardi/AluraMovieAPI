using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AluraMovieApi.Models;

public class Adress
{
    [Required]
    public int Id { get; set; }

    public string Street { get; set; }

    public string City  { get; set; }

    public string Number { get; set;}   

    public virtual Theater? Theater { get; set;}
     
}
