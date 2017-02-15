using RouteLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutesTable
{
    class Program
    {
        delegate string MyDelegate(string text);
        static void Main(string[] args)
        {

            MyDelegate initObj = Delegate.CreateDelegate(typeof(MyDelegate), typeof(MyClass), "Print") as MyDelegate;
            var res=initObj("addd");
            Console.WriteLine(res);
            Console.Read();


        }
    }
}
