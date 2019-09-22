using Data.Pikachu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            PikachuDataContext context = new PikachuDataContext();

            context.Database.CreateIfNotExists();

            context.SaveChanges();

            Console.WriteLine("Hello World");

            Console.ReadKey(true);

        }
    }
}
