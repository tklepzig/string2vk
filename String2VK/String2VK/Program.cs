using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace String2VK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set focus to any text input, text is send in 5 seconds...");
            Thread.Sleep(5000);
            new String2VK().SendText("Foo!");
            Console.WriteLine("Text sent.");
            Console.WriteLine("Press any key to quit...");
            Console.Read();
        }
    }
}
