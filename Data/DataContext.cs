
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using DemoMinimalAPI.Entities;

namespace DemoMinimalAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        // Lo que se pone en DbSet es lo que se va a mapear a la base de datos
        public DbSet<Book> Books { get; set; }

        public static DataContext CreateInMemoryBooksDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open(); // Importante: SQLite in-memory requiere mantener la conexión abierta

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .Options;

            var context = new DataContext(options);
            context.Database.EnsureCreated(); // Crea las tablas según el modelo

            return context;
        }
    };



}
