using Lend.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lend.Repository.Mapping
{
    public class InstallmentMap : EntityTypeConfiguration<Installment>
    {
        public InstallmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Payment)
                .IsRequired();

            this.Property(t => t.DueDateTime)
                .IsRequired();

            this.Property(t => t.PaidDateTime)
                .IsOptional();

            this.HasRequired(t => t.Loan)
                .WithMany(t => t.Installments);
        }

    }
}
