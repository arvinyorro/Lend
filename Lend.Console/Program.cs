using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lend.Domain;
using Lend.Repository;

namespace Lend.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            using(var db = new LendContext())
            {
                var borrower = new Borrower("Michael", "Yorro");
                db.Borrowers.Add(borrower);
                db.SaveChanges();

                var query = from x in db.Borrowers
                            select x;

                foreach (var x in query)
                {
                    System.Console.WriteLine(x.FirstName);
                }
            }
            System.Console.ReadLine();
        }
    }
}
