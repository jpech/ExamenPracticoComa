using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class ExamenDBContext : DbContext
    {
        public ExamenDBContext()
        {

        }

        public ExamenDBContext(DbContextOptions<ExamenDBContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Persona> Persona { get; set; }
    }
}
