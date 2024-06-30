using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using AluraMovieApi.Data;
using AluraMovieApi.Models;
using AluraMovieApi.Data.Dto;

namespace AluraMovieApi.Controller;

[ApiController]
[Route("[controller]")]
public class MovieSessionController : ControllerBase
{
    private MovieContext _context;

    private IMapper _mapper;

    public MovieSessionController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    /// <summary>
    /// Adiciona uma sessão de cinema ao banco de dados
    /// </summary>
    /// <param name="movieSessionDto">Objeto com os campos necessário para criação de uma sessão de cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso Inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AddMovieSession([FromBody] CreateMovieSessionDto movieSessionDto)
    {
        var movieSession = this._mapper.Map<MovieSession>(movieSessionDto);
        _context.MovieSessions.Add(movieSession);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetMovieSessionById), new { idmovie = movieSession.MovieId, idtheater = movieSession.TheaterId }, movieSession);
    }

    /// <summary>
    /// Busca sessão de cinemas no banco de dados
    /// </summary>
    /// <param name="skip">pagina inicial, por padrão 0</param>
    /// <param name="take">quantidade de cinemas carregados, por padrão 10 </param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna todas as sessão de cadastrados, respeitando paginação</response>
    [HttpGet]
    public IEnumerable<ReadMovieSessionDto> GetAllTMovieSession([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var movieSessionListDataBase = _context.MovieSessions.Skip(skip).Take(take).ToList();
        var movieSessionList = _mapper.Map<List<ReadMovieSessionDto>>(movieSessionListDataBase);
        return movieSessionList;
    }

    /// <summary>
    /// Busca uma sessão de cinemas no banco de dados por id
    /// </summary>
    /// <param name="id">id da sessão de cinemas</param>    
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna uma sessão de cinemas respeitando o id informado</response>
    [HttpGet("{idmovie}/{idtheater}")]
    public IActionResult GetMovieSessionById(int idMovie, int idTheater)
    {
        var session = _context.MovieSessions.FirstOrDefault(s => s.MovieId == idMovie && s.TheaterId == idTheater);

        if (session is null) return NotFound();

        var sessionDto = _mapper.Map<ReadMovieSessionDto>(session);

        return Ok(sessionDto);
    }

    /// <summary>
    /// Remove um cinemas do banco de dados respeitando o id informado
    /// </summary>
    /// <param name="id">id do cinema</param>    
    /// <returns>IActionResult</returns>
    /// <response code="204">remove o cinema respeitando o id informado</response>
    [HttpDelete("{idmovie}/{idtheater}")]
    public IActionResult DeleteMovieSession(int idMovie, int idTheater)
    {
        var session = _context.MovieSessions.FirstOrDefault(s => s.MovieId == idMovie && s.TheaterId == idTheater);

        if (session is null) return NotFound();

        _context.MovieSessions.Remove(session);
        _context.SaveChanges();

        return NoContent();
    }

}
