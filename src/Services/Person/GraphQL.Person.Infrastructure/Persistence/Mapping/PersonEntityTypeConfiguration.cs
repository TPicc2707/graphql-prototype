using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQL.Person.Infrastructure.Persistence.Mapping
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Person>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Person> builder)
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
