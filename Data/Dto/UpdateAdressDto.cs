using System.ComponentModel.DataAnnotations;

namespace AluraMovieApi.Data.Dto;

public class UpdateAdressDto
{
    [Required]
    public int Id { get; set; }

    public string Street { get; set; }

    public string City  { get; set; }

    public string Number { get; set;}    

}