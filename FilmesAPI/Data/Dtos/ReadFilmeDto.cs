using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FilmesAPI.Data.Dtos;

public class ReadFilmeDto
{
    public String Titulo { get; set; }
    public String Genero { get; set; }
    public int Duracao { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
