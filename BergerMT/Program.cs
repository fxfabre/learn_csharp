using System;
using System.Threading;

namespace BergerMT
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var manageur = new Manageur();
            manageur.Run();

            Console.WriteLine("end !");
            Console.ReadLine();
        }
    }
}