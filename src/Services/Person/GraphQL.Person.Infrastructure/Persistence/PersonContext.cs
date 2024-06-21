using Microsoft.EntityFrameworkCore;

namespace GraphQL.Person.Infrastructure.Persistence
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Domain.Entities.Person> People { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonContext).Assembly);
        }
    }
}
