using Microsoft.EntityFrameworkCore;

namespace L01_2020CC605.Models
{
    public class calificacionesContext : DbContext
    {
        public calificacionesContext(DbContextOptions<calificacionesContext> options) :base(options) 
        {
        }
        public DbSet<calificaciones> calificaciones { get; set; }

        public DbSet<usuarios> usuarios { get; set; }

        public DbSet<comentarios> comentarios { get; set; }


    }
}
