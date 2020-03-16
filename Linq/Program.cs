using System;
using System.Collections.Generic;
using NLog;

namespace TestLinq
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var requests = new RunRequest();
            var result = requests.Run(RunRequest.RequestId.Request2);

            IEnumerable<object> list = result;
            foreach (var o in list)
            {
                Console.WriteLine(o.ToString());
            }

            Console.ReadLine();
        }
    }
}