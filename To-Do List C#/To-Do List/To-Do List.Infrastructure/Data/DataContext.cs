using Microsoft.EntityFrameworkCore;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Infrastructure.Data
{
    /// <summary>
    /// La conexion con la Base de datos de los diferentes Entities de la capa de Domain.
    /// Anexo de una base de datos semilla.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
