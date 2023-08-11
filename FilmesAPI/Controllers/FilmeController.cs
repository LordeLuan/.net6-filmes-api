using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")] // Define a rota do endpoint como o nome da classe sem o "controller"
public class FilmeController : ControllerBase
{

    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost] //Especifica o verbo HTTP
    public IActionResult AdicionaFilme([FromBody] Filme filme) //anotacao fromBody para parametro
    {
        filme.Id = id++;
        filmes.Add(filme);
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme); // retorna 201 com o objeto criado e o location no header
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return filmes.Skip(skip).Take(take);
    }

    // Interrogacao especifica que pode retornar valor nulo ou do tipo Filme
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound(); // retorna o codigo 404
        return Ok(filme); // retorna 200 e o objeto
    }
}

