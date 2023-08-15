using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost] //Especifica o verbo HTTP
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) //anotacao fromBody para parametro
    {
        // Converte de DTO para entidade com o autoMapper
        Filme filme = _mapper.Map<Filme>(filmeDto); 
        
        _context.Filmes.Add(filme);
        _context.SaveChanges(); // Para salvar/commitar o registro criado
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme); // retorna 201 com o objeto criado e o location no header
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    // Interrogacao especifica que pode retornar valor nulo ou do tipo Filme
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound(); // retorna o codigo 404
        return Ok(filme); // retorna 200 e o objeto
    }
}

