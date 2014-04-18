using Lend.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lend.Repository.Mapping
{
    public class ExpenseMap : EntityTypeConfiguration<Expense>
    {
        public ExpenseMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Amount)
                .IsRequired();

            this.Property(t => t.AddedDateTIme)
                .IsRequired();
        }
    }
}
