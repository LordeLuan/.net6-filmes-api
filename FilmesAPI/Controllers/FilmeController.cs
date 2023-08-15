using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")] // Define a rota do endpoint como o nome da classe sem o "controller"
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper) // Injetando o FilmeContext ao iniciar a classe
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost] //Especifica o verbo HTTP
    [ProducesResponseType(StatusCodes.Status201Created)] //documentacao com swagger
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) //anotacao fromBody para parametro
    {
        // Converte de DTO para entidade com o autoMapper
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges(); // Para salvar/commitar o registro criado
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme); // retorna 201 com o objeto criado e o location no header
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadFilmeDto>>( //Converte todos os itens da lista para Dto
            _context.Filmes.Skip(skip).Take(take)
            ); 
    }

    // Interrogacao especifica que pode retornar valor nulo ou do tipo Filme
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound(); // retorna o codigo 404

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme); // Converte de entidade para Dto

        return Ok(filmeDto); // retorna 200 e o objeto
    }


    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto) //anotacao fromBody para parametro
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        // Converte de DTO para entidade com o autoMapper
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges(); // Para salvar/commitar o registro criado
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch) //anotacao fromBody para parametro
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        // Converte de DTO para entidade com o autoMapper
        var filmeParAtualizarParcial = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeParAtualizarParcial, ModelState);

        if (!TryValidateModel(filmeParAtualizarParcial))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParAtualizarParcial, filme);
        _context.SaveChanges(); // Para salvar/commitar o registro criado
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluiFilme(int id) //anotacao fromBody para parametro
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        // Converte de DTO para entidade com o autoMapper
        _context.Remove(filme);
        _context.SaveChanges(); // Para salvar/commitar o registro criado
        return NoContent();
    }
}

