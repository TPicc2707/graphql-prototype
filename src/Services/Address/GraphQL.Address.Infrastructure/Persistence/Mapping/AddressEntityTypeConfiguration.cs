using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQL.Address.Infrastructure.Persistence.Mapping
{
    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Address>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(i => i.AddressId)
                .HasName("Address_ID");

            builder.Property(s => s.PersonId)
                .HasColumnName("Person_ID");

            builder.Property(s => s.Type)
                .HasColumnName("Type")
                .IsRequired();

            builder.Property(s => s.Street)
               .HasColumnName("Street")
               .IsRequired();

            builder.Property(s => s.City)
               .HasColumnName("City")
               .IsRequired();

            builder.Property(s => s.State)
                .HasColumnName("State")
                .IsRequired();

            builder.Property(s => s.ZipCode)
                .HasColumnName("Zip_Code")
                .IsRequired()
                .HasMaxLength(5);
        }
    }
}
