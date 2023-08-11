using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{

    public int Id { get; set; }

    [Required(ErrorMessage = "O titulo do filme é obrigatório!")]
    [StringLength(30, ErrorMessage = "Limite máximo de caracteres é 30!")]
    public String Titulo { get; set; }

    [Required(ErrorMessage = "O genero do filme é obrigatório!")]
    [MaxLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres!")]
    public String Genero { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatório!")]
    [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minutos!")]
    public int Duracao { get; set; }

}

