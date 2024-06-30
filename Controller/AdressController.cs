using AluraMovieApi.Data;
using AluraMovieApi.Data.Dto;
using AluraMovieApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AluraMovieApi.Controller;

[ApiController]
[Route("[controller]")]
public class AdressController : ControllerBase
{
   private MovieContext _context;

   private IMapper _mapper;

    public AdressController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    /// <summary>
    /// Adiciona um endereço ao banco de dados
    /// </summary>
    /// <param name="adressDto">Objeto com os campos necessário para criação do endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso Inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AddAdress([FromBody] CreateAdressDto adressDto)
    {
        var adress = this._mapper.Map<Adress>(adressDto);
        _context.Adresses.Add(adress);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetAdressById), new { id = adress.Id }, adress);
    }

    /// <summary>
    /// Busca endereço do cinema no banco de dados
    /// </summary>
    /// <param name="skip">pagina inicial, por padrão 0</param>
    /// <param name="take">quantidade de endereço do cinema carregados, por padrão 10 </param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna todos os endereço do cinema cadastrados, respeitando paginação</response>
    [HttpGet]
    public IEnumerable<ReadAdressDto> GetAllAdresses([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadAdressDto>>(_context.Adresses.Skip(skip).Take(take));
    }

    /// <summary>
    /// Busca um endereço do cinema no banco de dados por id
    /// </summary>
    /// <param name="id">id do endereço do cinema</param>    
    /// <returns>IActionResult</returns>
    /// <response code="200">Retorna o endereço do cinema respeitando o id informado</response>
    [HttpGet("{id}")]
    public IActionResult GetAdressById(int id)
    {
        var adress = _context.Adresses.FirstOrDefault(m => m.Id == id);

        if (adress is null) return NotFound();

        var adressDto = _mapper.Map<ReadAdressDto>(adress);

        return Ok(adressDto);
    }

    /// <summary>
    /// Atualiza dados de um endereço do cinema no banco de dados por id
    /// </summary>
    /// <param name="id">id do endereço do cinema</param>    
    /// <param name="adressDto">Objeto com os campos necessário para atualização do endereço do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Retorna o endereço do cinema respeitando o id informado</response>
    [HttpPut("{id}")]
    public IActionResult UpdateAdress(int id, [FromBody] UpdateAdressDto adressDto)
    {
        var adress = _context.Adresses.FirstOrDefault(m => m.Id == id);

        if (adress is null) return NotFound();

        _mapper.Map(adressDto, adress);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza, parcialmente, dados de um endereço do cinema no banco de dados por id
    /// </summary>
    /// <param name="id">id do endereço do cinema</param>    
    /// <param name="adressDto">Objeto com os campos necessário para atualização do endereço do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Retorna o endereço do cinema respeitando o id informado</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateAdressPartial(int id, JsonPatchDocument<UpdateAdressDto> patch)
    {
        var adress = _context.Adresses.FirstOrDefault(m => m.Id == id);

        if (adress is null) return NotFound();

        var adressToUpdate = _mapper.Map<UpdateAdressDto>(adress);

        patch.ApplyTo(adressToUpdate, ModelState);

        if (!TryValidateModel(adressToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(adressToUpdate, adress);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Remove um endereço do cinema do banco de dados respeitando o id informado
    /// </summary>
    /// <param name="id">id do endereço do cinema</param>    
    /// <returns>IActionResult</returns>
    /// <response code="204">remove o endereço do cinema respeitando o id informado</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteAdress(int id)
    {
        var adress = _context.Adresses.FirstOrDefault(m => m.Id ==id);

        if (adress is null) return NotFound();

        _context.Adresses.Remove(adress)        ;
        _context.SaveChanges();

        return NoContent();
    }

}