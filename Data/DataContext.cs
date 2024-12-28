using DemoMinimalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMinimalAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        // Lo que se pone en DbSet es lo que se va a mapear a la base de datos
        public DbSet<Persona> Personas { get; set; }
    }
}
