using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using AluraMovieApi.Data;
using AluraMovieApi.Models;
using AluraMovieApi.Data.Dto;


namespace AluraMovieApi.Controller;

[ApiController]
[Route("[controller]")]

public class TheaterController : ControllerBase
{
    private MovieContext _context;

    private IMapper _mapper;

    public TheaterController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    /// <summary>
    /// Adiciona um cinema ao banco de dados
    /// </summary>
    /// <param name="theaterDto">Objeto com os campos necessário para criação do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso Inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AddTheater([FromBody] CreateTheaterDto theaterDto)
    {
        var theater = this._mapper.Map<Theater>(theaterDto);
        _context.Theaters.Add(theater);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTheaterById), new { id = theater.Id }, theater);
    }

    /// <summary>
    /// Busca cinemas no banco de dados
    /// </summary>
    /// <param name="skip">pagina inicial, por padrão 0</param>
    /// <param name="take">quantidade de cinemas carregados, por padrão 10 </param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna todos os cinemas cadastrados, respeitando paginação</response>
    [HttpGet]
    public IEnumerable<ReadTheaterDto> GetAllTheaters([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var theaterListDataBase = _context.Theaters.Skip(skip).Take(take).ToList();
        var theaterList = _mapper.Map<List<ReadTheaterDto>>(theaterListDataBase);
        return theaterList;
    }

    /// <summary>
    /// Busca um cinemas no banco de dados por id
    /// </summary>
    /// <param name="id">id do cinema</param>    
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna o cinema respeitando o id informado</response>
    [HttpGet("{id}")]
    public IActionResult GetTheaterById(int id)
    {
        var theater = _context.Theaters.FirstOrDefault(t => t.Id == id);

        if (theater is null) return NotFound();

        var theaterDto = _mapper.Map<ReadTheaterDto>(theater);

        return Ok(theaterDto);
    }

    /// <summary>
    /// Atualiza dados de um cinemas no banco de dados por id
    /// </summary>
    /// <param name="id">id do cinema</param>    
    /// <param name="theaterDto">Objeto com os campos necessário para atualização do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Retorna o cinema respeitando o id informado</response>
    [HttpPut("{id}")]
    public IActionResult UpdateTheater(int id, [FromBody] UpdateTheaterDto theaterDto)
    {
        var theater = _context.Theaters.FirstOrDefault(m => m.Id == id);

        if (theater is null) return NotFound();

        _mapper.Map(theaterDto, theater);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza, parcialmente, dados de um cinemas no banco de dados por id
    /// </summary>
    /// <param name="id">id do cinema</param>    
    /// <param name="theaterDto">Objeto com os campos necessário para atualização do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Retorna o cinema respeitando o id informado</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateTheaterPartial(int id, JsonPatchDocument<UpdateTheaterDto> patch)
    {
        var theater = _context.Theaters.FirstOrDefault(m => m.Id == id);

        if (theater is null) return NotFound();

        var theaterToUpdate = _mapper.Map<UpdateTheaterDto>(theater);

        patch.ApplyTo(theaterToUpdate, ModelState);

        if (!TryValidateModel(theaterToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(theaterToUpdate, theater);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Remove um cinemas do banco de dados respeitando o id informado
    /// </summary>
    /// <param name="id">id do cinema</param>    
    /// <returns>IActionResult</returns>
    /// <response code="204">remove o cinema respeitando o id informado</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteTheater(int id)
    {
        var theater = _context.Theaters.FirstOrDefault(m => m.Id ==id);

        if (theater is null) return NotFound();

        _context.Theaters.Remove(theater);
        _context.SaveChanges();

        return NoContent();
    }
}
