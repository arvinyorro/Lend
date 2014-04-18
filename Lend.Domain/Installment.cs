using System;

namespace Lend.Domain
{
    public class Installment
    {
        public Installment(Loan loan, DateTime dueDate, decimal payment)
        {
            this.Loan = loan;
            this.Payment = payment;
            this.DueDateTime = dueDate;
        }

        protected Installment()
        {

        }

        public int ID { get; set; }
        public decimal Payment { get; set; }
        public DateTime DueDateTime { get; set; }
        public DateTime? PaidDateTime { get; set; }
        public virtual Loan Loan { get; set; }
    }
}
