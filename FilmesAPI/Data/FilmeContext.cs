using FilmesAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;
// Faz conexão com o banco de dados
//O DbContext serve como ponte e para fazer operações no banco.
public class FilmeContext : DbContext
{

    public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts) // Casa o parametro para o construtor da classe DbContext
    {

    }

    public DbSet<Filme> Filmes { get; set; }



}
