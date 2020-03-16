using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factory
{
    class Program
    {
        static void Main()
        {
            var factory = new MyFactory();
            factory.MainMenu();
        }

    }
}
