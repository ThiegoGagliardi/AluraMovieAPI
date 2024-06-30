namespace AluraMovieApi.Data.Dto;

public class ReadTheaterDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ReadAdressDto Adress { get; set; }

    public List<ReadMovieSessionDto>? MovieSession { get; set; }

}