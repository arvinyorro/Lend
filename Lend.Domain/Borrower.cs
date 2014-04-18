using System;
using System.Collections.Generic;   

namespace Lend.Domain
{
    public class Borrower
    {
        public Borrower(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.RegisteredDateTime = DateTime.Now;
            this.Loans = new List<Loan>();
        }

        protected Borrower()
        {

        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDateTime { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
