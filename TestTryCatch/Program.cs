using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                f();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception dans le main : " + e.Message);
            }
            finally
            {
                Console.WriteLine("Finally du main");
            }

            Console.ReadLine();
        }


        static void f()
        {
            try
            {
                throw new FileNotFoundException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new StackOverflowException();
            }
            finally
            {
                Console.WriteLine("Je suis passé dans le finally de f");
            }


        }
    }
}
