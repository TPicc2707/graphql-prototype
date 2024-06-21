using GraphQL.Address.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQL.Infrastructure.Persistence.Mapping
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(i => i.PersonId)
                .HasName("Person_ID");

            builder.Property(s => s.FirstName)
                .HasColumnName("First_Name")
                .IsRequired();

            builder.Property(s => s.MiddleInitial)
               .HasColumnName("Middle_Initial")
               .HasPrecision(1);

            builder.Property(s => s.LastName)
               .HasColumnName("Last_Name")
               .IsRequired();

            builder.Property(s => s.Title)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(4);
        }

    }
}
