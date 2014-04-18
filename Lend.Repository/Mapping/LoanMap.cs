using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lend.Domain;

namespace Lend.Repository.Mapping
{
    public class LoanMap : EntityTypeConfiguration<Loan>
    {
        public LoanMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.Property(t => t.AmountBorrowed)
                .IsRequired();

            this.Property(t => t.Interest)
                .IsRequired();

            this.Property(t => t.AmountPayment)
                .IsRequired();

            this.Property(t => t.BorrowedDateTime)
                .IsRequired();

            this.HasRequired(t => t.Borrower)
                .WithMany(t => t.Loans);

            this.HasOptional(t => t.Expense)
                .WithRequired();

            this.HasMany(t => t.Installments)
                .WithRequired(t => t.Loan);
        }
    }
}
