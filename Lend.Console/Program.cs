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
                var query = from x in db.Borrowers
                            select x;

                if (query.Count() == 0)
                {
                    System.Console.WriteLine("No borrowers");
                }

                foreach (var x in query)
                {
                    System.Console.WriteLine(x.FirstName);
                }
            }
            System.Console.ReadLine();
        }
    }
}
