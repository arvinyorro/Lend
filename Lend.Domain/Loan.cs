using System;
using System.Collections.Generic;

namespace Lend.Domain
{
    public class Loan
    {
        private decimal interestRate = (decimal)0.1;

        protected Loan()
        {

        }

        public Loan(
            decimal amount, 
            int installmentsCount, 
            Expense expense = null)
        {
            this.Paid = false;
            this.BorrowedDateTime = DateTime.Now;
            this.AmountBorrowed = amount;
            this.Expense = expense;

            this.Interest = amount * this.interestRate;
            this.TotalPayment = amount + this.Interest;

            this.CreateInstallments(installmentsCount);
        }

        private void CreateInstallments(int installmentsCount)
        {
            this.Installments = new List<Installment>();
            decimal installmentAmount = this.TotalPayment / installmentsCount;
            DateTime dueDate = DateTime.Now;
            for (int x = 1; x <= installmentsCount; x++)
            {
                var installment = new Installment(this, dueDate, installmentAmount);
                this.Installments.Add(installment);

                dueDate.AddDays(1);
            }
        }

        public int ID { get; set; }
        public decimal AmountBorrowed { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment { get; set; }
        public bool Paid { get; set; }
        public DateTime BorrowedDateTime { get; set; }
        public virtual Borrower Borrower { get; set; }
        public virtual Expense Expense { get; set; }
        public virtual ICollection<Installment> Installments { get; private set; }
    }
}
