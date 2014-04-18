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
    public class BorrowerMap : EntityTypeConfiguration<Borrower>
    {
        public BorrowerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasMany(t => t.Loans)
                .WithRequired(t => t.Borrower);
        }
    }
}
