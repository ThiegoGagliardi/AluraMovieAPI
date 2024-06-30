using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

using AluraMovieApi.Models;
using AluraMovieApi.Data;
using AluraMovieApi.Data.Dto;

namespace AluraMovieApi.Controller;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;

    private IMapper _mapper;

    public MovieController(MovieContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="movieDto">Objeto com os campos necessário para criação do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso Inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AddMovie([FromBody] CreateMovieDto movieDto)
    {
        var movie = this._mapper.Map<Movie>(movieDto);
        _context.Movies.Add(movie);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    /// <summary>
    /// Busca filmes no banco de dados
    /// </summary>
    /// <param name="skip">pagina inicial, por padrão 0</param>
    /// <param name="take">quantidade de filmes carregados, por padrão 10 </param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna todos os filmes cadastrados, respeitando paginação</response>
    [HttpGet]
    public IEnumerable<ReadMovieDto> GetAllMovies([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take).ToList());
    }

    /// <summary>
    /// Busca um filmes no banco de dados por id
    /// </summary>
    /// <param name="id">id do filme</param>    
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna o filme respeitando o id informado</response>
    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

        if (movie is null) return NotFound();

        var movieDto = _mapper.Map<ReadMovieDto>(movie);

        return Ok(movieDto);
    }

    /// <summary>
    /// Atualiza dados de um filmes no banco de dados por id
    /// </summary>
    /// <param name="id">id do filme</param>    
    /// <param name="movieDto">Objeto com os campos necessário para atualização do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Retorna o filme respeitando o id informado</response>
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

        if (movie is null) return NotFound();

        _mapper.Map(movieDto, movie);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza, parcialmente, dados de um filmes no banco de dados por id
    /// </summary>
    /// <param name="id">id do filme</param>    
    /// <param name="movieDto">Objeto com os campos necessário para atualização do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Retorna o filme respeitando o id informado</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateMoviePartial(int id, JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

        if (movie is null) return NotFound();

        var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);

        patch.ApplyTo(movieToUpdate, ModelState);

        if (!TryValidateModel(movieToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Remove um filmes do banco de dados respeitando o id informado
    /// </summary>
    /// <param name="id">id do filme</param>    
    /// <returns>IActionResult</returns>
    /// <response code="204">remove o filme respeitando o id informado</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Id ==id);

        if (movie is null) return NotFound();

        _context.Movies.Remove(movie)        ;
        _context.SaveChanges();

        return NoContent();
    }
}
