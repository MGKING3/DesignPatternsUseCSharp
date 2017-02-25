using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mg.Test.DesignPatterns
{
    class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**********TestStrategyPattern**********");
            TestStrategyPattern.test();
            Console.WriteLine("****************************************\n");

            Console.WriteLine("**********TestObserverPattern**********");
            TestObserverPattern.test();
            Console.WriteLine("****************************************\n");

            Console.ReadKey();
        }
    }
}
