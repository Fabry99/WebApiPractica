using Microsoft.EntityFrameworkCore;

namespace WebApiPractica.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options) { 
        
        }

        public DbSet<equipos>equipos { get; set; }

        public DbSet<marcas> marcas { get; set; }

        public DbSet<tipo_equipo> tipo_equipo { get; set; }

        public DbSet<estados_equipos>estados_equipo { get; set; }

        public DbSet<estados_reserva> estados_reservas {get; set; }

    }
}
