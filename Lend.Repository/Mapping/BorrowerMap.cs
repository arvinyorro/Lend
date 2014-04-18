using Lend.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lend.Repository.Mapping
{
    public class BorrowerMap : EntityTypeConfiguration<Borrower>
    {
        public BorrowerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation(
                    "Index", 
                    new IndexAnnotation(
                        new IndexAttribute("IX_FirstNameLastName", 1) { IsUnique = true }));

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation("Index", 
                    new IndexAnnotation(
                        new IndexAttribute("IX_FirstNameLastName", 2) { IsUnique = true }));

            this.Property(t => t.RegisteredDateTime)
                .IsRequired();

            this.HasMany(t => t.Loans)
                .WithRequired(t => t.Borrower);
        }
    }
}
