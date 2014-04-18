using System;

namespace Lend.Domain
{
    public class Expense
    {
        protected Expense()
        {

        }

        public Expense(decimal amount)
        {
            this.Amount = amount;
            this.AddedDateTIme = DateTime.Now;
        }

        public int ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime AddedDateTIme { get; set; }
    }
}
