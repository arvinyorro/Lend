using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lend.Domain;

namespace Lend.Console.Mapping
{
    public class InstallmentMap : EntityTypeConfiguration<Installment>
    {
        public InstallmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(t => t.Loan)
                .WithMany(t => t.Installments);
        }
    }
}
