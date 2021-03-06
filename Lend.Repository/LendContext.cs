﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lend.Domain;
using Lend.Repository.Mapping;

namespace Lend.Repository
{
    public class LendContext : DbContext
    {
        public LendContext()
            : base("name=LendContext")
        {

        }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Installment> Installments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BorrowerMap());
        }
    }
}
