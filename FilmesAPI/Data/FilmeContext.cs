using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {

        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts) // Casa o parametro para o construtor da classe DbContext
        {

        }

        public DbSet<Filme> Filmes { get; set; }



    }
}
