namespace AluraMovieApi.Data.Dto;

    public class ReadMovieDto
    {

        public string Title { get; set; }

        public string Genre { get; set; }

        public int Runtime { get; set; }

        public DateTime RequestDateTime { get; set; } =  DateTime.Now;

        public List<ReadMovieSessionDto> MovieSessions { get; set; }

    }
