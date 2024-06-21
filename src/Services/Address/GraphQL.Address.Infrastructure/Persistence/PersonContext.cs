using GraphQL.Address.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Address.Infrastructure.Persistence
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Domain.Entities.Address> Addresses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonContext).Assembly);
        }
    }
}
